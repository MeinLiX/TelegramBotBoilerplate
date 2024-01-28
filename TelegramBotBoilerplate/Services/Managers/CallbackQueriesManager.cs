using TelegramBotBoilerplate.Abstract;
using TelegramBotBoilerplate.Services.CallbackQueryHandlers;
using Telegram.Bot.Types;


namespace TelegramBotBoilerplate.Services.Managers;

public class CallbackQueriesManager(IServiceProvider serviceProvider,
                                    IConfiguration configuration,
                                    ILogger<CallbackQueriesManager> _logger)
             : ManagerBase<ICallbackQueryHandler>(serviceProvider,
                                                  configuration)
{
    private async Task<bool> TryExecute<T>(CallbackQuery callbackQuery,
                                           CancellationToken cancellationToken)
        where T : ICallbackQueryHandler
        => GetHandler<T>().CanExecute(callbackQuery) switch
        {
            null => false,
            { } res =>
            await ((Func<Task<bool>>)(async () =>
            {
                await res.HandleAsync(callbackQuery, cancellationToken);
#if DEBUG
                _logger.LogInformation($"Invoke {nameof(T)} with data {callbackQuery.Data}");
#endif
                return true;
            })).Invoke()
        };  // when case '{} =>' just invoke handle method and return true

    public async Task ExecuteCallbackQueryHandler(CallbackQuery callbackQuery,
                                                  CancellationToken cancellationToken)
    {
        if (await TryExecute<GetOwnChannelsMenuCallbackQueryHandler>(callbackQuery, cancellationToken)) return;
    }
}