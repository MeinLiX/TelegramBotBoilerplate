using Telegram.Bot.Types;

namespace TelegramBotBoilerplate.Abstract;

public interface ICommandHandler : IHandler
{
    /// <summary>
    /// Execute response of request
    /// </summary>
    public Task<Message> HandleAsync(Message message, CancellationToken cancellationToken);
}