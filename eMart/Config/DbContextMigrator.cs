using eMart_Repository.Migrations;
using eMart_Repository.Repository;
using System.Diagnostics.Metrics;

namespace eMart.Config
{
    public static class DbContextMigrator
    {
        public static IServiceCollection Migrate(this IServiceCollection services)
        {
            
            return services;
        }
    }
}
