using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Caching;
using Volo.Abp.Localization;

namespace OOD.WeddingPlanner.LanguageTexts
{
    public class DbLocalizationResourceContributor : ILocalizationResourceContributor
    {
        public ILanguageTextRepository LanguageTextRepository { get; set; }
        public IDistributedCache<List<LanguageText>> DistributedCache { get; set; }
        public IDistributedCache<LanguageText> SingleDistributedCache { get; set; }

        public DbLocalizationResourceContributor()
        {
        }

        public void Fill(string cultureName, Dictionary<string, LocalizedString> dictionary)
        {
            foreach (var item in DistributedCache.GetOrAdd(cultureName, () =>
            {
                var task = LanguageTextRepository.GetListAsync(p => p.CultureName == cultureName);
                task.Wait();
                return task.Result;
            }))
            {
                dictionary[item.Name] = new LocalizedString(item.Name, item.Value, true);
            }
        }

        public LocalizedString GetOrNull(string cultureName, string name)
        {
            var item = SingleDistributedCache.GetOrAdd(cultureName + "_" + name, () =>
            {
                var task = LanguageTextRepository.GetListAsync(p => p.CultureName == cultureName && p.Name == name);
                task.Wait();
                return task.Result.FirstOrDefault();
            });
            if(item == null) return null;
            return new LocalizedString(item.Name, item.Value, true);
        }

        public void Initialize(LocalizationResourceInitializationContext context)
        {
            LanguageTextRepository = context.ServiceProvider.GetService<ILanguageTextRepository>();
            DistributedCache = context.ServiceProvider.GetService<IDistributedCache<List<LanguageText>>>();
            SingleDistributedCache = context.ServiceProvider.GetService<IDistributedCache<LanguageText>>();
        }
    }
}
