using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Porter.Common.Domain.Interfaces;

namespace Porter.Common.EF.Repository
{
    public static class IoC
    {

        public static IServiceCollection AddEFLogRepository(this IServiceCollection services)
        {
            return services.AddScoped<ILogRepository, LogRepository>();
        }


           
        public static IServiceCollection AddPostgresDbContext<CustomDbContext>(this IServiceCollection services, IConfiguration configuration) where CustomDbContext : DbContextBase
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CustomDbContext>(options =>
            {
                options.LogTo(Console.WriteLine, LogLevel.Information);
                options.UseNpgsql(connectionString,
                    npgsqlOptions =>
                    {
                        //npgsqlOptions.EnableRetryOnFailure(maxRetryCount: 2);
                    })
                .UseLazyLoadingProxies();
            });

            services.AddScoped<DbContextBase>(sp => sp.GetRequiredService<CustomDbContext>());

            return services.AddEFLogRepository();
        }


    }
}
