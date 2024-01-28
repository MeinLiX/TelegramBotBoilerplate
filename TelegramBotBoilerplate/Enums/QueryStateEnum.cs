namespace TelegramBotBoilerplate.Enums;

public enum QueryStateEnum
{
    Undefined = 0,


    // 1 - command states
    GetChannelIdCommand = 1,
    GetOwnChannelsMenuCommand = 2,



    // 100 - callback states
    GetOwnChannelsMenuCallbackQuery = 100,



    // 1000 - additional fn
    GoBack = 1000,
    Refresh = 1001

}