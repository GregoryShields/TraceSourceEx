# TraceSource Example Project

See
[How to: Use TraceSource and Filters with Trace Listeners](https://msdn.microsoft.com/en-us/library/ms228993%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396)
and
[How to: Create and Initialize Trace Sources](https://msdn.microsoft.com/en-us/library/ms228984(v=vs.110).aspx).

See App.config for listener declarations.

Using a configuration file to set up listeners allows turning them on and off without having to redeploy the application, simply by modifying the config file.

See [Trace Switches](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/trace-switches)
and [How to: Create, Initialize and Configure Trace Switches](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/how-to-create-initialize-and-configure-trace-switches).


## Discussion

Without specifying any trace listeners or filters, trace messages will be written to the default trace listener, which is usually the Console window. To initialize the listeners and filters, we define them in our configuration file.

To start with, the program creates a TraceSource object named __mySource from the trace source which is defined in App.config.

Looking at App.config, we see that our TraceSource object has three properties defined:
name="traceSourceEx",
switchName="sourceSwitch",
and switchType=SourceSwitch.

We also see that traceSourceEx has two trace listeners declared, with the default trace listener having been removed.

We can further see that App.config has exactly one trace switch declared with name "sourceSwitch".

A trace source, represented by the [TraceSource class](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.tracesource), has exactly one trace switch, represented by its [Switch property](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.tracesource.switch?view=net-6.0#System_Diagnostics_TraceSource_Switch) which is of type [SourceSwitch](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.sourceswitch?view=net-6.0).


### Trace Listeners

The first listener is named "console" and is declared to be of type ConsoleTraceListener.
It has exactly one filter of type EventTypeFilter which has a value of "Warning".

The second listener...


### Trace Switch

Although there are 3 types of trace switches in .NET (BooleanSwitch, TraceSwitch, and SourceSwitch), the Switch property of TraceSource is always of type SourceSwitch. I verified this in the debugger's watch window.


