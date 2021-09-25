using System;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        private static ILogger<ConfigureRepository> _logger;

        public ConfigureRepository(ILogger<ConfigureRepository> logger)
        {
            _logger = logger;
        }

        public static void ConfigureDependencyRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserRepository>();

            var MYSQL_CONNECTION = Environment.GetEnvironmentVariable("MYSQL_CONNECTION");
            var MYSQL_DATABASE = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            var dbVersion = Environment.GetEnvironmentVariable("MYSQL_VERSION");

            var connection = MYSQL_CONNECTION + $";Database={MYSQL_DATABASE};";
            _logger.LogInformation($"connectionstring--> {connection}");

            serviceCollection.AddDbContext<MyContext>(o => o.UseMySql(connection, new MySqlServerVersion(new Version(dbVersion))));
        }

        public static void ConfigureMgrationDatabase(IServiceScope service)
        {
            _logger.LogInformation("Migração da base..");

            using (var context = service.ServiceProvider.GetService<MyContext>())
            {
                context.Database.Migrate();
            }
        }
    }
}