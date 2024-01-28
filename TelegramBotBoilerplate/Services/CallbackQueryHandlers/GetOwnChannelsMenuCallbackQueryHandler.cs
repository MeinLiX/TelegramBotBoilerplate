using TelegramBotBoilerplate.Abstract;
using TelegramBotBoilerplate.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotBoilerplate.Services.CallbackQueryHandlers;

public class GetOwnChannelsMenuCallbackQueryHandler(ITelegramBotClient botClient)
             : HandlerBase(botClient, QueryStateEnum.GetOwnChannelsMenuCallbackQuery)
             , ICallbackQueryHandler
{

    public ICallbackQueryHandler? CanExecute(CallbackQuery callbackQuery)
    {
        if (callbackQuery is not { } || string.IsNullOrEmpty(callbackQuery.Data))
            return null;

        var inputData = ((ICallbackQueryHandler)this).ParseData(callbackQuery);

        if (inputData.NextState == GetActualState)
            return this;

        if (inputData.CurrentState == GetActualState
            && inputData.NextState == QueryStateEnum.Refresh)
            return this;

        return null;
    }

    public async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        var inputData = ((ICallbackQueryHandler)this).ParseData(callbackQuery);

        await _botClient.AnswerCallbackQueryAsync(
        callbackQueryId: callbackQuery.Id,
        text: $"{callbackQuery.Data}{Environment.NewLine}{inputData.GetCallbackData}",
        cancellationToken: cancellationToken);

        await Task.Delay(2000);
    }
}