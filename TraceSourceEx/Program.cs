using System.Diagnostics;

namespace TraceSourceEx
{
	class Program
	{
		static readonly TraceSource __mySource;

		static Program() // Private static constructor.
		{
			__mySource = new TraceSource("traceSourceEx");
		}

		static void Main()
		{
			Activity1();

			Activity2();

			Activity3();

			__mySource.Close();
		}

		static void Activity1()
		{
			// Raise events.
			__mySource.TraceEvent(TraceEventType.Error, 1, "Error message.");
			__mySource.TraceEvent(TraceEventType.Warning, 2, "Warning message.");
			__mySource.TraceInformation("Informational message.");
		}

		static void Activity2()
		{
			// Change the event type for which tracing occurs.
			// The console trace listener must be specified in the configuration file.

			// First, save the original settings from the configuration file in a variable.
			var configFilter =
				__mySource.Listeners["console"].Filter as EventTypeFilter;

			// Then create a new event type filter that ensures critical messages will be written.
			__mySource.Listeners["console"].Filter =
				new EventTypeFilter(SourceLevels.Critical);

			// Raise events.
			__mySource.TraceEvent(TraceEventType.Critical, 3, "Critical message.");
			__mySource.TraceInformation("Informational message.");
		}

		static void Activity3()
		{
			// Allow the trace source to send messages to listeners for all event types.
			// This statement will override any settings in the configuration file.
			__mySource.Switch.Level = SourceLevels.All;

			// Restore the original filter settings.
			__mySource.Listeners["console"].Filter = configFilter;

			// Raise events.
			__mySource.TraceEvent(TraceEventType.Error, 4, "Error message.");
			__mySource.TraceInformation("Informational message.");
		}
	}
}
