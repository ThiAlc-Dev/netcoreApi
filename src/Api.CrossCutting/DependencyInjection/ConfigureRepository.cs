using System;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependencyRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserRepository>();

            var MYSQL_CONNECTION = Environment.GetEnvironmentVariable("MYSQL_CONNECTION");
            var MYSQL_DATABASE = Environment.GetEnvironmentVariable("MYSQL_DATABASE");

            var connection = MYSQL_CONNECTION + $";Database={MYSQL_DATABASE};";
            serviceCollection.AddDbContext<MyContext>(o => o.UseMySql(connection));
        }

        public static void ConfigureMgrationDatabase(IServiceScope service)
        {
            using (var context = service.ServiceProvider.GetService<MyContext>())
            {
                context.Database.Migrate();
            }
        }
    }
}