using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OOD.WeddingPlanner.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class WeddingPlannerDbContextFactory : IDesignTimeDbContextFactory<WeddingPlannerDbContext>
    {
        public WeddingPlannerDbContext CreateDbContext(string[] args)
        {
            WeddingPlannerEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<WeddingPlannerDbContext>()
                .UseSqlite(configuration.GetConnectionString("Default"));

            return new WeddingPlannerDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OOD.WeddingPlanner.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
