using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Porter.Application.Mapping;
using Porter.Application.Services;
using Porter.Application.Services.Interfaces;
using Porter.Application.Validators;
using Porter.Dto;

namespace Porter.Application
{
    public static class IoC
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IRoomService, RoomService>();
            return services.AddScoped<IClientService, ClientService>();

        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {


            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ClientProfile>();
                cfg.AddProfile<RoomProfile>();
                cfg.AddProfile < BookingProfile>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {

            services.AddScoped < IValidator<RequestUpdateBookingDto>, RequestUpdateBookingDtoValidator>();
            services.AddScoped<IValidator<RequestRegisterBookingDto>, RequestRegisterBookingDtoValidator>();
            
            services.AddScoped<IValidator<RequestRegisterRoomDto>, RequestRegisterRoomDtoValidator>();
            return services.AddScoped<IValidator<RequestRegisterClientDto>, RequestRegisterClientDtoValidator>();
        }

    }
}
