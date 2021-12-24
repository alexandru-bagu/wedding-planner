using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OOD.WeddingPlanner.Data
{
  /* This is used if database provider does't define
   * IWeddingPlannerDbSchemaMigrator implementation.
   */
  public class NullWeddingPlannerDbSchemaMigrator : IWeddingPlannerDbSchemaMigrator, ITransientDependency
  {
    public Task MigrateAsync()
    {
      return Task.CompletedTask;
    }
  }
}