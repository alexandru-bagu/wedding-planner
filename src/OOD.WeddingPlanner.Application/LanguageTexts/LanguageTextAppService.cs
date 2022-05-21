using System;
using OOD.WeddingPlanner.Permissions;
using OOD.WeddingPlanner.LanguageTexts.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OOD.WeddingPlanner.Localization;
using System.Globalization;
using Volo.Abp.Localization;
using System.Linq;
using System.Collections.Generic;
using Volo.Abp.Caching;

namespace OOD.WeddingPlanner.LanguageTexts
{
    public class LanguageTextAppService : CrudAppService<LanguageText, LanguageTextDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateLanguageTextDto, CreateUpdateLanguageTextDto>,
        ILanguageTextAppService
    {
        protected override string GetPolicyName { get; set; } = WeddingPlannerPermissions.LanguageText.Default;
        protected override string GetListPolicyName { get; set; } = WeddingPlannerPermissions.LanguageText.Default;
        protected override string CreatePolicyName { get; set; } = WeddingPlannerPermissions.LanguageText.Create;
        protected override string UpdatePolicyName { get; set; } = WeddingPlannerPermissions.LanguageText.Update;
        protected override string DeletePolicyName { get; set; } = WeddingPlannerPermissions.LanguageText.Delete;

        private readonly ILanguageTextRepository _repository;
        private readonly IStringLocalizer<WeddingPlannerResourceBase> _baseResources;
        private readonly IDistributedCache<List<LanguageText>> _distributedCache;
        private readonly IDistributedCache<LanguageText> _singleDistributedCache;

        public LanguageTextAppService(ILanguageTextRepository repository,
            IStringLocalizer<WeddingPlannerResourceBase> baseResources,
            IDistributedCache<List<LanguageText>> distributedCache,
            IDistributedCache<LanguageText> singleDistributedCache
            ) : base(repository)
        {
            _repository = repository;
            _baseResources = baseResources;
            _distributedCache = distributedCache;
            _singleDistributedCache = singleDistributedCache;
        }

        public async Task<PagedResultDto<LanguageTextDto>> GetAllListAsync(GetLanguageTextsInputDto input)
        {
            using (CultureHelper.Use(new CultureInfo(input.CultureName)))
            {
                var strings = new List<LocalizedString>(_baseResources.GetAllStrings(true, true));
                var count = strings.Count;
                bool hasFilter = !string.IsNullOrEmpty(input.Filter);
                var items = strings.WhereIf(hasFilter, p => p.Name.Contains(input.Filter) || p.Value.Contains(input.Filter)).Select(p => new LanguageTextDto() { Id = Guid.Empty, CultureName = input.CultureName, Name = p.Name, Value = p.Value });
                Dictionary<string, LanguageTextDto> dict = new Dictionary<string, LanguageTextDto>();
                foreach (var item in items) dict[item.Name] = item;
                var res = await _repository.GetListAsync(p => p.CultureName == input.CultureName && ((hasFilter && (p.Name.Contains(input.Filter) || p.Value.Contains(input.Filter))) || !hasFilter));
                foreach (var item in res) dict[item.Name] = ObjectMapper.Map<LanguageText, LanguageTextDto>(item);

                return new PagedResultDto<LanguageTextDto>()
                {
                    TotalCount = dict.Count,
                    Items = dict.Values.Skip(input.SkipCount).Take(input.MaxResultCount).OrderBy(p => p.Name).ToList()
                };
            }
        }

        public override async Task<LanguageTextDto> CreateAsync(CreateUpdateLanguageTextDto input)
        {
            var result = await base.CreateAsync(input);
            _singleDistributedCache.Remove(input.CultureName + "_" + input.Name);
            _distributedCache.Remove(input.CultureName);
            return result;
        }

        public override async Task<LanguageTextDto> UpdateAsync(Guid id, CreateUpdateLanguageTextDto input)
        {
            var result = await base.UpdateAsync(id, input);
            _singleDistributedCache.Remove(input.CultureName + "_" + input.Name);
            _distributedCache.Remove(input.CultureName);
            return result;
        }

        public override async Task DeleteAsync(Guid id)
        {
            var item = await base.GetAsync(id);
            await base.DeleteAsync(id);
            _singleDistributedCache.Remove(item.CultureName + "_" + item.Name);
            _distributedCache.Remove(item.CultureName);
        }
    }
}
