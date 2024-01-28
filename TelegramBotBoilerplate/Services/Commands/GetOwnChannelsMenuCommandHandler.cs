using TelegramBotBoilerplate.Abstract;
using TelegramBotBoilerplate.Enums;
using TelegramBotBoilerplate.Extensions;
using TelegramBotBoilerplate.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotBoilerplate.Services.CommandHandlers;

public class GetOwnChannelsMenuCommandHandler(ITelegramBotClient botClient)
             : HandlerBase(botClient, QueryStateEnum.GetOwnChannelsMenuCommand)
             , ICommandHandler
{

    public async Task<Message> HandleAsync(Message message, CancellationToken cancellationToken)
    {
        Dictionary<string, CallbackDataModel> dataForKeyboard = new();
        Dictionary<string, long> avaliableChannels = GetChannelsWithAdminRole();

        if (avaliableChannels.Count == 0)
        {
            return await _botClient.SendMessageWithRefreshKeyboard(message,
                                                                  QueryStateEnum.GetOwnChannelsMenuCallbackQuery,
                                                                  "Not found channels where bot have admin role",
                                                                  cancellationToken);
        }

        //cast avaliableChannels to dataForKeyboard

        return await _botClient.SendTextMessageAsync(
               chatId: message.Chat.Id,
               replyToMessageId: message.MessageId,
               text: "Select channel for open menu:",
               replyMarkup: GetAvaliableChannels(dataForKeyboard).AddRefreshButtonToFooter(message.From!.Id, QueryStateEnum.GetOwnChannelsMenuCallbackQuery),
               parseMode: ParseMode.Markdown,
               cancellationToken: cancellationToken);
    }



    /// <summary>
    /// Get Dictionary<channel_name,channel_id> where bot & user have admin role
    /// </summary>
    private Dictionary<string, long> GetChannelsWithAdminRole()
    {
        Dictionary<string, long> result = new();
	//LOGIC
        return result;
    }

    /// <summary>
    /// Get keyboard with avaliable channels
    /// </summary>
    public static InlineKeyboardMarkup GetAvaliableChannels(Dictionary<string, CallbackDataModel> channels) => new(
           new[]
           {
                channels.Select(channel=>InlineKeyboardButton.WithCallbackData(channel.Key, channel.Value.GetCallbackData()))
                        .ToArray()
           });
}