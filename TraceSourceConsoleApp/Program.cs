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
		static void Main()
		{
			var mySource = new TraceSource("LogSource");

			mySource.TraceEvent(
				TraceEventType.Verbose,
				1,
				"Now we're logging!");

			// Allows passing an object, not just a string.
			mySource.TraceData(
				TraceEventType.Verbose,
				1,
				"Now we're logging!");

			//__traceSource.Flush(); // Unnecessary
			mySource.Close();
		}
	}
}
