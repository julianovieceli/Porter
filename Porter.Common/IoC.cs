using Microsoft.Extensions.DependencyInjection;
using Porter.Common.Services;

namespace Porter.Common
{
    public static class IoC
    {
        public static IServiceCollection AddLogService(this IServiceCollection services)
        {
            return services.AddScoped<ILogService, LogService>();
        }
    }

}
