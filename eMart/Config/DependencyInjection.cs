using eMart_Repository.Migrations;
using eMart_Repository.Repository;
using Microsoft.EntityFrameworkCore;

namespace eMart.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<EMartDbContext>(options => options.UseSqlServer(GetConnectionString()));
            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {            
            return services;
        }

        private static string GetConnectionString()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config["ConnectionStrings:DefaultConnection"];

            return strConn;
        }
    }

}
