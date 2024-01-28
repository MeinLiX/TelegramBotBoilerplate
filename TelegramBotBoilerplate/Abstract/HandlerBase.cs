using TelegramBotBoilerplate.Enums;
using Telegram.Bot;


namespace TelegramBotBoilerplate.Abstract;

public abstract class HandlerBase(ITelegramBotClient botClient, 
                                  QueryStateEnum actualState) 
                      : IHandler
{
    protected ITelegramBotClient _botClient = botClient;

    public QueryStateEnum GetActualState => actualState;
}