using System;
using System.Diagnostics;

/* Code from article "How to: Create and Initialize Trace Sources"
https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/how-to-create-and-initialize-trace-sources
With a trace source we can trace events and data.
We can also output informational traces.

TraceFilter: https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.tracefilter
"Trace filters can be used by trace listeners to provide an extra layer of filtering beyond that provided by trace switches."

The way it all works is this:
Trace sources define a switch. 
Trace sources also define listeners, which in turn each can optionally define a filter.
Listeners are constrained both by the switch and their respective filters.
Whichever of the two is most restrictive, either the switch or the filter, defines the events which are output by the listener.
This situation may be complicated in cases where a switch or a filter defines multiple values rather than just one.
I suppose the strategy for configuring these things on the fly would be that you change the switch value when you want to affect
all the listeners, and you change the filter for a given listener for which you want to make a change.
But if the switch is more restrictive than a given filter, then the switch rules.
Conversely, if a given filter value is more restrictive than the value set for the switch, then the filter rules.
That's why it's called a filter, because it always subtracts allowable output from the switch value, but cannot add to it.
Yet this same logic goes conversely for the switch value as well.

I suppose the philosophy behind all of this is that you start out with the ability to output every kind of event, and then you
have a switch which can restrict that. Then you also have a filter which can further restrict it. But of course if your filter
is not more restrictive than the switch, then it has no effect.
So it's probably a good practice to set the switch at the least restrictive level that you want to allow, and then apply a
filter in cases where you want a particular listener to be more restrictive than the level you've set on your switch.

Maybe you want your log file not to record information events, but you want your console to report those along with events of a
higher level. In that case, you set your log file's filter to a level of Warning and you set no filter at all on your console
listener.
*/

namespace TraceSourceEx
{
	class Program
	{
		static void Main()
		{
			// This trace source initializes two listeners:
			// a text writer trace listener (named "mySharedListener") that writes a file named "mySharedListener.log"
			// a console trace listener (named "console") that writes to the console
			// An EventTypeFilter (which inherits TraceFilter) is created for each of the two listeners:
			// "console" defines a filter which traces at the Error level.
			var myTraceSource = new TraceSource("myTraceSource");

			Activity1(myTraceSource);

			Activity2(myTraceSource);

			Activity3(myTraceSource);

			myTraceSource.Close();

			Console.ReadLine();
		}

		static void Activity1(TraceSource myTraceSource)
		{
			Console.WriteLine("Raise events as-is.");

			RaiseEvents(myTraceSource);

			Console.WriteLine();
		}

		static void Activity2(TraceSource myTraceSource)
		{
			Console.WriteLine("Change the console filter to critical, which in turn changes the event type for which tracing occurs.");

			// First save the original filter from the configuration file in a variable.
			var consoleFilter =
				myTraceSource.Listeners["console"].Filter as EventTypeFilter; // EventType = Error

			// Then create a new event type filter that ensures critical messages will be written.
			myTraceSource.Listeners["console"].Filter =
				new EventTypeFilter(SourceLevels.Critical); // EventType = Critical

			RaiseEvents(myTraceSource);

			// Restore the original filter settings.
			myTraceSource.Listeners["console"].Filter = consoleFilter;

			Console.WriteLine();
		}

		static void Activity3(TraceSource myTraceSource)
		{
			Console.WriteLine("Change the switch's level to All");
			// Allow the trace source to send messages to listeners for all event types.
			// This statement will override the setting in the configuration file.
			myTraceSource.Switch.Level = SourceLevels.All;

			RaiseEvents(myTraceSource);
		}

		static void RaiseEvents(TraceSource myTraceSource)
		{
			// Raise events.
			myTraceSource.TraceEvent(
				TraceEventType.Critical,
				1,
				"Critical message.");

			myTraceSource.TraceEvent(
				TraceEventType.Error,
				2,
				"Error message.");

			myTraceSource.TraceEvent(
				TraceEventType.Warning,
				3,
				"Warning message.");

			myTraceSource.TraceInformation(
				"Informational message.");
		}
	}
}
