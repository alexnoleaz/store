using Microsoft.Extensions.Configuration;
using Store.Shared.Validations;

namespace Store.Shared.Repositories.EntityFrameworkCore.Configuration;

public class EntityFrameworkCoreConfiguration : IEntityFrameworkCoreConfiguration
{
    private static EntityFrameworkCoreConfiguration? _instance;
    private static readonly object _lock = new();

    private readonly IConfiguration _configuration;

    private EntityFrameworkCoreConfiguration(IConfiguration configuration)
    {
        ArgumentValidator.NotNull(configuration);
        _configuration = configuration;
    }

    public static EntityFrameworkCoreConfiguration GetInstance(IConfiguration configuration)
    {
        if (_instance is null)
            lock (_lock)
                if (_instance is null)
                    _instance = new EntityFrameworkCoreConfiguration(configuration);

        return _instance;
    }

    public string GetConnectionString() =>
        _configuration.GetRequiredSection("ConnectionStrings:Default").Value!;
}
