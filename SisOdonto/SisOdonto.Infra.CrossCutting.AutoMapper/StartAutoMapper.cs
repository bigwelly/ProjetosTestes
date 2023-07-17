using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SisOdonto.Infra.CrossCutting.AutoMapper
{
    public static class StartAutoMapper
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntityToDTOMapper());
                mc.AddProfile(new DTOToEntityMapper());                
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
