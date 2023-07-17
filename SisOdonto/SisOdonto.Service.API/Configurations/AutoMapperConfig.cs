using System;
using SisOdonto.Infra.CrossCutting.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SisOdonto.Service.API.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.ConfigureAutoMapper();
        }
    }
}