using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Porter.Domain.Interfaces;
using Porter.Infra.Postgres.Repository.Repository;

namespace Porter.Infra.Postgres.Repository
{
    public static class IoC
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration )
        {
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            return services.AddScoped<IClientRepository, ClientRepository>();
        }

    }
}
