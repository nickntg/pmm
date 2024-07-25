using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using pmm.core.Models;
using pmm.core.Services;
using pmm.core.Services.Interfaces;
using pmm.data.Entities;

namespace pmm.core.Configuration
{
    public static class Extensions
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            services.AddSingleton(ConfigureMapper());

            return services;
        }

        public static IMapper ConfigureMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MobileDataDto, MobileData>().ReverseMap();
            });

            return new Mapper(config);
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMobileDataService, MobileDataService>();

            return services;
        }
    }
}
