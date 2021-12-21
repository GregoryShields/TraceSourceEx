using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace core_console_logging;

class Program
{
	static void Main()
	{
		var (logger, fooLogger) = GetLoggers();

		// Settings in appsettings.json filter this out.
		logger.LogInformation(
			1,
			"Logger information.");

		logger.LogWarning(
			2,
			"Logger warning.");

		IFooService fooService = new FooService(fooLogger);
		fooService.DoWork();
	}

	static Tuple<ILogger, ILogger> GetLoggers()
	{
		// .NET Core requires that we explicitly load configuration files.
		var configuration = new ConfigurationBuilder()
			.AddJsonFile(
				"appsettings.json",
				false,
				true)
			.Build();

		// See the 3.0 example which confirms our code:
		// https://docs.microsoft.com/en-us/aspnet/core/migration/logging-nonaspnetcore?view=aspnetcore-6.0
		using var loggerFactory = LoggerFactory.Create(
			builder =>
			{
				builder.AddConfiguration(
					configuration.GetSection("Logging")
				);
				builder.AddConsole();
			}
		);

		// Create two different loggers and return them.
		var logger    = loggerFactory.CreateLogger<Program>();
		var fooLogger = loggerFactory.CreateLogger<FooService>();

		return new Tuple<ILogger, ILogger>(logger, fooLogger);
	}
}
