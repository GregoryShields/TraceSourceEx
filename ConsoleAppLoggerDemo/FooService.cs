using Microsoft.Extensions.Logging;

namespace ConsoleAppLoggerDemo;

public class FooService : IFooService
{
	readonly ILogger<FooService> _logger;

	public FooService(ILogger<FooService> logger)
	{
		_logger = logger;
	}

	public void FooServiceLoggingExample()
	{
		_logger.LogInformation(
			3,
			"Doing some work.");

		_logger.LogWarning(
			4,
			"Some kind of warning.");

		_logger.LogCritical(
			5,
			"Something critical!");
	}
}
