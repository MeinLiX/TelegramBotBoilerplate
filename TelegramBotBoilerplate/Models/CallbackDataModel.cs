using TelegramBotBoilerplate.Enums;

namespace TelegramBotBoilerplate.Models;

public class CallbackDataModel
{
    public long? UserOwnerId { get; set; }

    public long? SelectedChannelId { get; set; }

    public QueryStateEnum CurrentState { get; set; }
    public QueryStateEnum NextState { get; set; }

    //public UserStateEnum UserState {get;set;} IN FUTURE

    public CallbackDataModel() { }
    public CallbackDataModel(string data)
    {
        try
        {
            FillFromCallbackData(data);
        }
        catch { }
    }

    public bool OwnerRequest(long? userInvokerId)
        => UserOwnerId.HasValue && UserOwnerId != 0 && UserOwnerId == userInvokerId.GetValueOrDefault();

    /// <summary>
    /// Parse object to callbackdata
    /// </summary>
    public string GetCallbackData()
        => $"{UserOwnerId}{Constants.SPLITTER_CALLBACK}{SelectedChannelId}{Constants.SPLITTER_CALLBACK}{(int)CurrentState}{Constants.SPLITTER_CALLBACK}{(int)NextState}";

    /// <summary>
    /// Parse string for fill object fills
    /// </summary>
    /// <param name="data"></param>
    private void FillFromCallbackData(string data)
    {
        var items = data.Split(Constants.SPLITTER_CALLBACK);

        if (long.TryParse(items[0], out long userOwnerId))
            UserOwnerId = userOwnerId;

        if (long.TryParse(items[1], out long selectedChannelId))
            SelectedChannelId = selectedChannelId;

        if (int.TryParse(items[2], out int currentState))
            CurrentState = (QueryStateEnum)currentState;
        else CurrentState = QueryStateEnum.Undefined;

        if (int.TryParse(items[3], out int nextState))
            NextState = (QueryStateEnum)nextState;
        else NextState = QueryStateEnum.Undefined;
    }
}