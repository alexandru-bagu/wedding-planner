using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace OOD.WeddingPlanner.Web
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddApplication<WeddingPlannerWebModule>();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.InitializeApplication();
    }
  }
}
