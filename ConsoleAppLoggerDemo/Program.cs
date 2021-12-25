// Install these two NuGet packages...
// Microsoft.Extensions.Configuration.Json
// Microsoft.Extensions.Logging.Console
// ...and then reference them in these two usings...
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleAppLoggerDemo;

class Program
{
	static void Main()
	{
		var loggerFactory = CreateLoggerFactory();

		// First logging example.
		var logger = loggerFactory.CreateLogger<Program>();
		ProgramClassLoggingExample(logger);

		// Second logging example.
		var fooLogger = loggerFactory.CreateLogger<FooService>();
		IFooService fooService = new FooService(fooLogger);
		fooService.FooServiceLoggingExample();
	}

	static void ProgramClassLoggingExample(ILogger<Program> logger)
	{
		// This is filtered out by settings in appsettings.json.
		logger.LogInformation(
			1,
			"Logger information.");

		logger.LogWarning(
			2,
			"Logger warning.");
	}

	static ILoggerFactory CreateLoggerFactory()
	{
		// .NET Core requires that we explicitly load configuration files.
		var configuration = new ConfigurationBuilder()
			.AddJsonFile(
				"appsettings.json",
				false,
				true)
			.Build();

		// In Microsoft.Extensions.Logging 3.0 or greater you use the Create method, which is different than
		// the way it was done in 2.1 or 2.2.
		// https://docs.microsoft.com/en-us/aspnet/core/migration/logging-nonaspnetcore?view=aspnetcore-6.0
		// We've installed version 6.0 in this app.
		var loggerFactory = LoggerFactory.Create(
			builder =>
			{
				builder.AddConfiguration(
					configuration.GetSection("Logging")
				);
				builder.AddConsole();
			}
		);
		return loggerFactory;
	}
}
