using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                options.UseNpgsql(connectionString,
                    npgsqlOptions =>
                    {
                        //npgsqlOptions.EnableRetryOnFailure(maxRetryCount: 2);
                    });
            });

            services.AddScoped<IRoomRepository, RoomRepository>();
            return services.AddScoped<IClientRepository, ClientRepository>();
        }

    }
}
