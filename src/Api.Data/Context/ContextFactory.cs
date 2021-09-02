using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var MYSQL_CONNECTION = Environment.GetEnvironmentVariable("MYSQL_CONNECTION");
            var MYSQL_DATABASE = Environment.GetEnvironmentVariable("MYSQL_DATABASE");

            var connection = MYSQL_CONNECTION + $";Database={MYSQL_DATABASE};";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(connection)
                    .EnableSensitiveDataLogging(true)
                    .EnableDetailedErrors();

            return new MyContext(optionsBuilder.Options);
        }
    }
}