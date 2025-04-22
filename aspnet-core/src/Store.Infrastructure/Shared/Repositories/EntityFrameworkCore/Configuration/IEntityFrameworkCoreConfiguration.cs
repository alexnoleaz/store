namespace Store.Shared.Repositories.EntityFrameworkCore.Configuration;

public interface IEntityFrameworkCoreConfiguration
{
    string GetConnectionString();
}