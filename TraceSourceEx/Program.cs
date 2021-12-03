using System.Diagnostics;

// https://msdn.microsoft.com/en-us/library/ms228993%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
// https://msdn.microsoft.com/en-us/library/ms228984(v=vs.110).aspx

// See App.config for listener declarations.
// Using a configuration file to set up listeners allows turning them on and off
// with having to redeploy the application, simply by modifying the config file.
// See https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/trace-switches
// and https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/how-to-create-initialize-and-configure-trace-switches

namespace TraceSourceEx
{
	class Program
	{
		static readonly TraceSource __mySource;

		static Program() // Private static constructor.
		{
			// Create a TraceSource object named __mySource from the trace source named "TraceSourceEx"
			// which is defined in App.config.

			// Looking at App.config, we see that TraceSourceEx has two trace listeners declared and the
			// default trace listener has been removed. A trace source can only have one trace switch, and
			// we see that TraceSourceEx has exactly one trace switch declared.

			// Trace Listeners
			// The first listener is named "console" and is declared to be of type ConsoleTraceListener.
			// It has exactly one filter of type EventTypeFilter which has a value of "Warning".

			// Trace Switch
			// There are 3 types of trace switches in .NET: BooleanSwitch, TraceSwitch, and SourceSwitch.
			// The trace switch defined in TraceSourceEx is of type SourceSwitch. I verified this in the
			// debugger's watch window. It makes sense that a TraceSource object would have a trace switch
			// of type SourceSwitch.

			__mySource = new TraceSource("TraceSourceEx");
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
