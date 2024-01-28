using TelegramBotBoilerplate.Abstract;
using Telegram.Bot;

namespace TelegramBotBoilerplate.Services;

public class ReceiverService(ITelegramBotClient botClient,
                             UpdateHandler updateHandler,
                             ILogger<ReceiverServiceBase<UpdateHandler>> logger)
             : ReceiverServiceBase<UpdateHandler>(botClient,
                                                  updateHandler,
                                                  logger)
{
}
