using System;
using SisOdonto.Infra.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace SisOdonto.Service.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services, configuration);
        }
    }
}