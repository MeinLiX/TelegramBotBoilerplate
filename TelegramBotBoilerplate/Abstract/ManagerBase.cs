namespace TelegramBotBoilerplate.Abstract;

public abstract class ManagerBase<TypeManagement>(IServiceProvider serviceProvider,
                                                  IConfiguration configuration)
    where TypeManagement : IHandler
{
    protected readonly IServiceProvider _serviceProvider = serviceProvider;
    protected readonly IConfiguration _configuration = configuration;

    public TypeManagement GetHandler<T>() where T : TypeManagement
        => _serviceProvider.GetService<T>() ?? throw new Exception($"'{nameof(TypeManagement)}': '{nameof(T)}' not registred");

}