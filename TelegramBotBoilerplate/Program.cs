using Telegram.Bot;
using TelegramBotBoilerplate.Services;
using TelegramBotBoilerplate.Helpers;
using TelegramBotBoilerplate.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var botUserName = configuration.GetConfigurationValue("tg:BotUserName");
        var botToken = configuration.GetConfigurationValue("tg:BotToken");

        services.AddWindowsService(options =>
        {
            options.ServiceName = $".NET '{botUserName}' telegram bot service";
        });

        services.RegisterTelegramBotCommands();

        services.AddHttpClient("telegram_bot_client")
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    TelegramBotClientOptions options = new(botToken);
                    return new TelegramBotClient(options, httpClient);
                });

        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddHostedService<WindowsBackgroundService>();
    })
    .Build();

await host.RunAsync();

