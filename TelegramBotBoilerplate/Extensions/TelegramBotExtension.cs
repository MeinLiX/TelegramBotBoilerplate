using TelegramBotBoilerplate.Enums;
using TelegramBotBoilerplate.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotBoilerplate.Extensions;


public static class TelegramBotExtension
{
    public static Task<Message> SendMessageWithRefreshKeyboard(
        this ITelegramBotClient telegramBotClient,
        Message message,
        QueryStateEnum needStateForRefresh,
        string textMessage = "Please refresh query",
        CancellationToken cancellationToken = default)
    {
        CallbackDataModel callbackData = new()
        {
            UserOwnerId = message.From!.Id,
            CurrentState = needStateForRefresh,
            NextState = QueryStateEnum.Refresh
        };

        return telegramBotClient.SendTextMessageAsync(
               chatId: message.Chat.Id,
               replyToMessageId: message.MessageId,
               text: textMessage,
               replyMarkup: GetRefreshKeyboard(callbackData),
               parseMode: ParseMode.Markdown,
               cancellationToken: cancellationToken);
    }

    public static InlineKeyboardMarkup AddRefreshButtonToFooter(this InlineKeyboardMarkup inlineKeyboardMarkup, long userId, QueryStateEnum needStateForRefresh)
    {
        CallbackDataModel callbackData = new()
        {
            UserOwnerId = userId,
            CurrentState = needStateForRefresh,
            NextState = QueryStateEnum.Refresh
        };

        return inlineKeyboardMarkup.AddRefreshButtonToFooter(callbackData);
    }

    public static InlineKeyboardMarkup AddRefreshButtonToFooter(this InlineKeyboardMarkup inlineKeyboardMarkup, CallbackDataModel callbackData)
    {
        if (inlineKeyboardMarkup is null) inlineKeyboardMarkup = GetRefreshKeyboard(callbackData);
        else inlineKeyboardMarkup = new(inlineKeyboardMarkup.InlineKeyboard.Append(new[] { InlineKeyboardButton.WithCallbackData("Refresh", callbackData.GetCallbackData()) }));

        return inlineKeyboardMarkup;
    }

    public static InlineKeyboardMarkup GetRefreshKeyboard(CallbackDataModel callbackData)
    => new(new[]
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Refresh", callbackData.GetCallbackData())
                }
            });
}