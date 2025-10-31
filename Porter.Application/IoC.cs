﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Porter.Application.Mapping;
using Porter.Application.Services;
using Porter.Application.Services.Interfaces;

namespace Porter.Application
{
    public static class IoC
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            return services.AddScoped<IUserPorterService, UserPorterService>();

        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {


            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserPorterProfile>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
