using Microsoft.Extensions.Configuration;

namespace Store.Shared.Repositories.EntityFrameworkCore;

public class EFCoreConfiguration
{
    public string ConnectionString { get; }

    public EFCoreConfiguration(IConfiguration configuration) =>
        ConnectionString = configuration.GetRequiredSection("ConnectionStrings:Default").Value!;
}
