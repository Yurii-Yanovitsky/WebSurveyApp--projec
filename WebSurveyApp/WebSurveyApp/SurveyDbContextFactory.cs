using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using WebLogic;

namespace WebSurveyApp
{
    class DesignTimeSurveyDbContextFactory : IDesignTimeDbContextFactory<SurveyDbContext>
    {
        public SurveyDbContext CreateDbContext(string[] args)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionBuilder = new DbContextOptionsBuilder();
            var option = optionBuilder
                .UseSqlServer(connectionString)
                .Options;

            return new SurveyDbContext(option);
        }
    }
}
