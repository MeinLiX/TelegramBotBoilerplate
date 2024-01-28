using TelegramBotBoilerplate.Enums;

namespace TelegramBotBoilerplate.Abstract;

public interface IHandler
{
    /// <summary>
    /// CONSTANT NAME OF HANDLER STATE
    /// </summary>
    public QueryStateEnum GetActualState { get; }
}