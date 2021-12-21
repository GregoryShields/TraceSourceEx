using System.Diagnostics;

namespace TraceSourceEx
{
	class Program
	{
		static void Main()
		{
			var mySource = new TraceSource("traceSourceEx");;

			Activity1(mySource);

			Activity2(mySource);

			Activity3(mySource);

			mySource.Close();
		}

		static void Activity1(TraceSource mySource)
		{
			// Raise events.
			mySource.TraceEvent(
				TraceEventType.Error,
				1,
				"Error message.");

			mySource.TraceEvent(
				TraceEventType.Warning,
				2,
				"Warning message.");

			mySource.TraceInformation(
				"Informational message.");
		}

		static void Activity2(TraceSource mySource)
		{
			// Change the event type for which tracing occurs.
			// The console trace listener must be specified in the configuration file.

			// First, save the original settings from the configuration file in a variable.
			var configFilter =
				mySource.Listeners["console"].Filter as EventTypeFilter;

			// Then create a new event type filter that ensures critical messages will be written.
			mySource.Listeners["console"].Filter =
				new EventTypeFilter(SourceLevels.Critical);

			// Raise events.
			mySource.TraceEvent(
				TraceEventType.Critical,
				3,
				"Critical message.");

			mySource.TraceInformation(
				"Informational message.");

			// Restore the original filter settings.
			mySource.Listeners["console"].Filter = configFilter;
		}

		static void Activity3(TraceSource mySource)
		{
			// Allow the trace source to send messages to listeners for all event types.
			// This statement will override any settings in the configuration file.
			mySource.Switch.Level = SourceLevels.All;

			// Raise events.
			mySource.TraceEvent(TraceEventType.Error, 4, "Error message.");
			mySource.TraceInformation("Informational message.");
		}
	}
}
