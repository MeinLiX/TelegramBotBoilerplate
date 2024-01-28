using TelegramBotBoilerplate.Abstract;

namespace TelegramBotBoilerplate.Services;

public class WindowsBackgroundService(IServiceProvider serviceProvider,
                                      ILogger<WindowsBackgroundService> logger)
             : PollingServiceBase<ReceiverService>(serviceProvider,
                                                   logger)
{
}