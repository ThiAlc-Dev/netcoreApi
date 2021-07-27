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

            if (Environment.GetEnvironmentVariable("DATABASE").ToLower().Equals("sqlserver"))
            {
                //TODO implementar conexão Sql Server
            }
            else
            {
                serviceCollection.AddDbContext<MyContext>(o =>
                o.UseMySql(Environment.GetEnvironmentVariable("DB_CONNNECTION")));
            }
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