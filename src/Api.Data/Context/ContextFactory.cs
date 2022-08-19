using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var connection = Environment.GetEnvironmentVariable("MYSQL_CONNECTION");
            var dbVersion = Environment.GetEnvironmentVariable("MYSQL_VERSION");

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(connection, new MySqlServerVersion(new Version(dbVersion)))
                    .EnableSensitiveDataLogging(true)
                    .EnableDetailedErrors();

            return new MyContext(optionsBuilder.Options);
        }
    }
}