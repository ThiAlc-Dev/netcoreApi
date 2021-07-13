using Api.CrossCutting.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureAutoMapper
    {
        public static void ConfigureAutoMapperDependency(IServiceCollection services)
        {
            var configureMapper = new AutoMapper.MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });

            var mapper = configureMapper.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}