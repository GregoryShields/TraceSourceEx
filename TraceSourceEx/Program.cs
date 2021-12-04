using System.Diagnostics;

namespace TraceSourceEx
{
	class Program
	{
		static readonly TraceSource __mySource;

		static Program() // Private static constructor.
		{
			// Create a TraceSource object named __mySource from the trace source which is defined in
			// App.config.

			// Looking at App.config, we see that our TraceSource object has name "traceSourceEx",
			// switchName "sourceSwitch", and switchType SourceSwitch.

			// We also see that traceSourceEx has two trace listeners declared and the default trace
			// listener has been removed. A trace source can only have one trace switch, and we see
			// that traceSourceEx has exactly one trace switch declared with name "sourceSwitch".

			// Trace Listeners
			// The first listener is named "console" and is declared to be of type ConsoleTraceListener.
			// It has exactly one filter of type EventTypeFilter which has a value of "Warning".

			// The second listener...

			// Trace Switch
			// There are 3 types of trace switches in .NET: BooleanSwitch, TraceSwitch, and SourceSwitch.
			// I verified that the trace switch defined in traceSourceEx is of type SourceSwitch in the
			// debugger's watch window.

			__mySource = new TraceSource("traceSourceEx");
		}

		static void Main()
		{
			Activity1();

			// Change the event type for which tracing occurs.
			// The console trace listener must be specified in the configuration file.

			// First, save the original settings from the configuration file in a variable.
			var configFilter =
				__mySource.Listeners["console"].Filter as EventTypeFilter;

			// Then create a new event type filter that ensures critical messages will be written.
			__mySource.Listeners["console"].Filter =
				new EventTypeFilter(SourceLevels.Critical);

			Activity2();

			// Allow the trace source to send messages to listeners for all event types.
			// This statement will override any settings in the configuration file.
			__mySource.Switch.Level = SourceLevels.All;

			// Restore the original filter settings.
			__mySource.Listeners["console"].Filter = configFilter;
			Activity3();

			__mySource.Close();
		}

		static void Activity1()
		{
			// Create our trace source...
			// Without identifying any trace listeners or filters, this code will cause trace messages
			// to be written to the default trace listener. To initialize the listeners and filters, we
			// define them in our configuration file.
			__mySource.TraceEvent(TraceEventType.Error, 1, "Error message.");
			__mySource.TraceEvent(TraceEventType.Warning, 2, "Warning message.");
			__mySource.TraceInformation("Informational message.");
		}

		static void Activity2()
		{
			__mySource.TraceEvent(TraceEventType.Critical, 3, "Critical message.");
			__mySource.TraceInformation("Informational message.");
		}

		static void Activity3()
		{
			__mySource.TraceEvent(TraceEventType.Error, 4, "Error message.");
			__mySource.TraceInformation("Informational message.");
		}
	}
}
