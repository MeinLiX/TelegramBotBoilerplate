using TelegramBotBoilerplate.Abstract;
using TelegramBotBoilerplate.Helpers;
using TelegramBotBoilerplate.Services.CommandHandlers;
using Telegram.Bot.Types;


namespace TelegramBotBoilerplate.Services.Managers;

public class CommandsManager(IServiceProvider serviceProvider,
                             IConfiguration configuration,
                             ILogger<CommandsManager> _logger)
             : ManagerBase<ICommandHandler>(serviceProvider,
                                            configuration)
{

    private string GetCommand(Message message)
    {
        if (message.Text is not { } messageText)
            throw new NullReferenceException($"Can't resolve command, missed: {nameof(message.Text)}");

        string command = messageText.Split(' ')
                          .Select(command => command.Contains(_configuration.GetConfigurationValue("tg:BotUserName"))
                                             ? command.Split('@').First() //Remove bot name if contain botName in command
                                             : command)
                          .First();
#if DEBUG
        _logger.LogInformation($"Entry command {command}");
#endif
        return command;
    }

    public Task<Message> GetCommandHandler(Message message, CancellationToken cancellationToken)
        => GetCommand(message) switch
        {
            "/id" => GetHandler<GetChannelIdCommandHandler>().HandleAsync(message, cancellationToken),
            "/menu" => GetHandler<GetOwnChannelsMenuCommandHandler>().HandleAsync(message, cancellationToken),
            _ => throw new NotImplementedException($"Not implement command: '{message.Text!.Split(' ')[0]}'"),
        };
}