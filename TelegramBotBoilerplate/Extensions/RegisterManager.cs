using TelegramBotBoilerplate.Services.CallbackQueryHandlers;
using TelegramBotBoilerplate.Services.CommandHandlers;
using TelegramBotBoilerplate.Services.Managers;

namespace TelegramBotBoilerplate.Extensions;

public static class RegisterManager
{
    public static IServiceCollection RegisterTelegramBotCommands(this IServiceCollection serviceCollection)
        => serviceCollection.AddSingleton<CommandsManager>()
                            .AddSingleton<GetChannelIdCommandHandler>()
                            .AddSingleton<GetOwnChannelsMenuCommandHandler>()
                            .AddSingleton<CallbackQueriesManager>()
                            .AddSingleton<GetOwnChannelsMenuCallbackQueryHandler>();
}