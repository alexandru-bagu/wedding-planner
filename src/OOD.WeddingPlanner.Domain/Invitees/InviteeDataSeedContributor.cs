using System.Collections.Generic;
using System.Threading.Tasks;
using OOD.WeddingPlanner.Invitees;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace OOD.WeddingPlanner.Invitations
{
    public class InviteeDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public InviteeDataSeedContributor(IInviteeRepository repository)
        {
            Repository = repository;
        }

        public IInviteeRepository Repository { get; }

        public async Task SeedAsync(DataSeedContext context)
        {
            var list = new List<Invitee>();
            foreach (var invitee in await Repository.GetListAsync())
            {
                if (!invitee.Child && string.IsNullOrWhiteSpace(invitee.Menu))
                {
                    invitee.Menu = "Adult";
                    list.Add(invitee);
                }
            }
            await Repository.UpdateManyAsync(list, true);
        }
    }
}