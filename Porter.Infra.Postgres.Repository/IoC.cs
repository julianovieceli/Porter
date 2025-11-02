using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Porter.Domain.Interfaces;
using Porter.Infra.Postgres.Repository.Repository;

namespace Porter.Infra.Postgres.Repository
{
    public static class IoC
    {
        public static IServiceCollection ConfigurePostGresDbContext(this IServiceCollection services, IConfiguration configuration )
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.LogTo(Console.WriteLine, LogLevel.Information);
                options.UseNpgsql(connectionString,
                    npgsqlOptions =>
                    {
                        //npgsqlOptions.EnableRetryOnFailure(maxRetryCount: 2);
                    })
                .UseLazyLoadingProxies();
            });

            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            return services.AddScoped<IClientRepository, ClientRepository>();
        }

    }
}
