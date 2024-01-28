using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TelegramBotBoilerplate.Abstract;

public abstract class ReceiverServiceBase<TUpdateHandler>(ITelegramBotClient botClient,
                                                          TUpdateHandler updateHandler,
                                                          ILogger<ReceiverServiceBase<TUpdateHandler>> _logger)
                      : IReceiverService
    where TUpdateHandler : IUpdateHandler
{
    private readonly ITelegramBotClient _botClient = botClient;
    private readonly IUpdateHandler _updateHandler = updateHandler;

    public async Task ReceiveAsync(CancellationToken stoppingToken)
    {
        var receiverOptions = new ReceiverOptions()
        {
            AllowedUpdates = [],
            ThrowPendingUpdates = true,
        };

        var me = await _botClient.GetMeAsync(stoppingToken);
        _logger.LogInformation("Start receiving updates for {BotName}", me.Username ?? "BOT_NAME_EMPTY");

        await _botClient.ReceiveAsync(
            updateHandler: _updateHandler,
            receiverOptions: receiverOptions,
            cancellationToken: stoppingToken);
    }
}