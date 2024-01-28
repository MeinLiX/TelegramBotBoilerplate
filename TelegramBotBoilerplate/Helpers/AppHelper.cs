namespace TelegramBotBoilerplate.Helpers;

public static class AppHelper
{
    public static string GetConfigurationValue(this IConfiguration? configuration, string key)
    {
        _ = configuration ?? throw new Exception("Configuration not found");
        var value = configuration[key];
        if (string.IsNullOrEmpty(value))
            throw new Exception($"Configuration key {key} is empty");
        return value;
    }
}