using TelegramBotBoilerplate.Models;
using Telegram.Bot.Types;

namespace TelegramBotBoilerplate.Abstract;

public interface ICallbackQueryHandler : IHandler
{
    /// <summary>
    /// Try parse <see cref="CallbackQuery.Data"/> into <see cref="CallbackDataModel"/>
    /// for execution <see cref="HandleAsync"/> <para/>
    /// Validation states inside parsed <see cref="CallbackDataModel"/>
    /// </summary>
    public ICallbackQueryHandler? CanExecute(CallbackQuery callbackQuery);

    /// <summary>
    /// Execute response of request
    /// </summary>
    public Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken);

    /// <summary>
    /// Try parse <see cref="CallbackQuery.Data"/> into <see cref="CallbackDataModel"/> inside model constructor
    /// </summary>
    public CallbackDataModel ParseData(CallbackQuery callbackQuery)
        => new(callbackQuery.Data!);
}