using Api.Data.Context;
using Api.Domain.Interfaces.Services;
using Api.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependencyServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddDbContext<MyContext>(o => o.UseMySql(configuration.GetConnectionString("dbApi")));
        }
    }
}