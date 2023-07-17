using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SisOdonto.Application.ApplicationServiceInterface;
using SisOdonto.Application.ApplicationServiceRepository;
using SisOdonto.Infra.CrossCutting.Identity.Authorization;
using SisOdonto.Infra.Data.Context;
using SisOdonto.Infra.Data.Interfaces;
using SisOdonto.Infra.Data.Repository;

namespace SisOdonto.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<SisOdontoContext>(options => options.EnableDetailedErrors()
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Infra - Data
            services.AddScoped<SisOdontoContext>();

            //Applications
            services.AddScoped<IAspNetUserTokenApplicationService, AspNetUserTokenApplicationService>();
            services.AddScoped<IAspNetUserApplicationService, AspNetUserApplicationService>();
            services.AddScoped<IClienteApplicationService, ClienteApplicationService>();
            services.AddScoped<ICepApplicationService, CepApplicationService>();

            //Infra - Data - Repository
            services.AddScoped<IAspNetUserTokensRepository, AspNetUserTokensRepository>();
            services.AddScoped<IAspNetUserRepository, AspNetUserRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICepRepository, CepRepository>();
        }
    }
}
