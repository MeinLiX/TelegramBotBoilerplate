using TelegramBotBoilerplate.Abstract;
using TelegramBotBoilerplate.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBotBoilerplate.Services.CommandHandlers;

public class GetChannelIdCommandHandler(ITelegramBotClient botClient)
             : HandlerBase(botClient, QueryStateEnum.GetChannelIdCommand)
             , ICommandHandler
{

    public async Task<Message> HandleAsync(Message message, CancellationToken cancellationToken)
    {
        return await _botClient.SendTextMessageAsync(
               chatId: message.Chat.Id,
               replyToMessageId: message.MessageId,
               text: Convert.ToString(message.Chat.Id),
               parseMode: ParseMode.Markdown,
               cancellationToken: cancellationToken);
    }
}