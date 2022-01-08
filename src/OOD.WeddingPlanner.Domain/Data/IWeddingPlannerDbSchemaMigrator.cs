using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Data
{
    public interface IWeddingPlannerDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
