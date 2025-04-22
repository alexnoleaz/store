using Microsoft.Extensions.Configuration;
using Store.Shared.Dependency;
using Store.Shared.Validations;

namespace Store.Shared.Repositories.EntityFrameworkCore.Configuration;

public class EntityFrameworkCoreConfiguration
    : IEntityFrameworkCoreConfiguration,
        ISingletonDependency
{
    private readonly IConfiguration _configuration;

    private EntityFrameworkCoreConfiguration(IConfiguration configuration)
    {
        ArgumentValidator.NotNull(configuration);
        _configuration = configuration;
    }

    public string GetConnectionString() =>
        _configuration.GetRequiredSection("ConnectionStrings:Default").Value!;
}
