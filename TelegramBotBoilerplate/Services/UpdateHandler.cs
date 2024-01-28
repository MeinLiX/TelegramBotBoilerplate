using TelegramBotBoilerplate.Services.Managers;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace TelegramBotBoilerplate.Services;

public class UpdateHandler(ILogger<UpdateHandler> logger,
                           CommandsManager commandsManager,
                           CallbackQueriesManager callbackQueriesManager)
             : IUpdateHandler
{
    private readonly ILogger<UpdateHandler> _logger = logger;
    private readonly CommandsManager _commandsManager = commandsManager;
    private readonly CallbackQueriesManager _callbackQueriesManager = callbackQueriesManager;

    public async Task HandleUpdateAsync(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
    {
        var handler = update switch
        {
            { Message: { } message } => BotOnMessageReceived(message, cancellationToken),
            { EditedMessage: { } message } => BotOnMessageReceived(message, cancellationToken),
            { CallbackQuery: { } callbackQuery } => BotOnCallbackQueryReceived(callbackQuery, cancellationToken),
            _ => UnknownUpdateHandlerAsync(update, cancellationToken)
        };

        await handler;
    }

    private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
    {
#if DEBUG
        _logger.LogInformation("Receive message type: {MessageType}", message.Type);
#endif
        Message sentMessage = await _commandsManager.GetCommandHandler(message, cancellationToken);
#if DEBUG
        _logger.LogInformation("The message was sent with id: {SentMessageId}", sentMessage.MessageId);
#endif
    }

    // Process Inline Keyboard callback data
    private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
#if DEBUG
        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);
#endif
        await _callbackQueriesManager.ExecuteCallbackQueryHandler(callbackQuery, cancellationToken);
    }


    private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
    {
#if DEBUG
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
#endif
        return Task.CompletedTask;
    }

    public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        if (exception is not NotImplementedException)
            _logger.LogInformation("HandleError: {ErrorMessage}", ErrorMessage);

        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }
}