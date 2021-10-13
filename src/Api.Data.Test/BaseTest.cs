using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {
            
        }

        public class DbTeste : IDisposable
        {
            private static string dbName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-","")}";

            private string connString = $"Persist Security Info=True;server=localhost;database={dbName};User=root;Password=13467900";
            private string dbVersion = "5.7.30";
            public ServiceProvider serviceProvider { get; private set; }

            public DbTeste()
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection.AddDbContext<MyContext>(c=> 
                c.UseMySql(connString, new MySqlServerVersion(new Version(dbVersion))),
                ServiceLifetime.Transient
                );

                serviceProvider = serviceCollection.BuildServiceProvider();
                using(var context = serviceProvider.GetService<MyContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
            
            public void Dispose()
            {
                using(var context = serviceProvider.GetService<MyContext>())
                {
                    context.Database.EnsureDeleted();
                }
            }
        }
    }
}
