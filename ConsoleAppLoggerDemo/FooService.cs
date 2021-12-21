using Microsoft.Extensions.Logging;

namespace core_console_logging;

public interface IFooService
{
	void DoWork();
}

public class FooService : IFooService
{
	readonly ILogger _logger;

	public FooService(ILogger logger)
	{
		_logger = logger;
	}

	public void DoWork()
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
