using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Shared.Repositories.EntityFrameworkCore;

namespace Store.Shared.Repositories;

public static class RepositoryConfigurer
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        EFCoreConfigurer.Configure(services, configuration);
    }
}