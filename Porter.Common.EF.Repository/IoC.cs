using Microsoft.Extensions.DependencyInjection;
using Porter.Domain.Interfaces;

namespace Porter.Common.EF.Repository
{
    public static class IoC
    {

        public static IServiceCollection AddEFLogRepository(this IServiceCollection services)
        {
            //return services.AddScoped<ILogRepository, LogRepository>();
            return services;
        }
        
            
    }
}
