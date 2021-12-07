using System;
using System.Diagnostics;

using Microsoft.Extensions.Hosting;
using var host = Host.CreateDefaultBuilder(args).Build();
await host.RunAsync();

// https://dotnetmeditations.com/blog/2013/08/11/using-tracesource-for-logging
namespace TraceSourceConsoleApp
{
	class Program
	{
		static readonly TraceSource __traceSource;

		static Program()
		{
			__traceSource = new TraceSource("LogSource");
		}

		static void Main()
		{
			// TraceEvent
			__traceSource.TraceEvent(
				TraceEventType.Verbose,
				1,
				"Now we're logging!");

			// TraceData - allows passing an object, not just a string.
			__traceSource.TraceData(
				TraceEventType.Verbose,
				1,
				"Now we're logging!");

			//__traceSource.Flush(); // Unnecessary
			__traceSource.Close();
		}
	}
}
