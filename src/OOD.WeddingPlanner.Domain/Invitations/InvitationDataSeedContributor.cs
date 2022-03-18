using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace OOD.WeddingPlanner.Invitations
{
    public class InvitationDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public InvitationDataSeedContributor(IInvitationRepository repository)
        {
            Repository = repository;
        }

        public IInvitationRepository Repository { get; }

        public async Task SeedAsync(DataSeedContext context)
        {
            var list = new List<Invitation>();
            foreach (var invitation in await Repository.GetListAsync())
            {
                if (invitation.UniqueCode == null)
                {
                    invitation.UniqueCode = await Invitation.GenerateUniqueCode(Repository);
                    list.Add(invitation);
                }
            }
            await Repository.UpdateManyAsync(list, true);
        }
    }
}