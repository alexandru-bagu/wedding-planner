using System;
using Volo.Abp.Domain.Repositories;

namespace OOD.WeddingPlanner.LanguageTexts
{
    public interface ILanguageTextRepository : IRepository<LanguageText, Guid>
    {
    }
}