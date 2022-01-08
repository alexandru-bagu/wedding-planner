using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner
{
    public static class IAuthorizationServiceExtensions
    {
        public static async Task AnyPolicy(this IAuthorizationService service, params string[] policy)
        {
            if (policy.Length != 0)
            {
                if (!await service.IsGrantedAnyAsync(policy))
                {
                    await service.CheckAsync(policy[0]);
                }
            }
        }
    }
}