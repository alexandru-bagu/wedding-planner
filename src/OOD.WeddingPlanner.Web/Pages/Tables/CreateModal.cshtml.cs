using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Tables;
using OOD.WeddingPlanner.Tables.Dtos;
using OOD.WeddingPlanner.Web.Pages.Tables.ViewModels;
using OOD.WeddingPlanner.Weddings;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OOD.WeddingPlanner.Web.Pages.Tables
{
    public class CreateModalModel : WeddingPlannerPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid WeddingId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid EventId { get; set; }

        [BindProperty]
        public CreateEditTableViewModel ViewModel { get; set; }

        private readonly ITableAppService _service;
        private readonly IWeddingAppService _weddingAppService;

        public CreateModalModel(ITableAppService service, IWeddingAppService weddingAppService, IEventAppService eventAppService)
        {
            _service = service;
            _weddingAppService = weddingAppService;
        }

        public virtual async Task OnGetAsync()
        {
            ViewModel = new CreateEditTableViewModel()
            {
                WeddingId = WeddingId,
                EventId = EventId
            };
            ViewModel.WeddingItems.AddRange(
              (await _weddingAppService.GetLookupListAsync(new LookupRequestDto()))
                .Items
                .Select(p => new SelectListItem(p.DisplayName, p.Id.ToString())));
            ViewModel.EventItems.Add(new SelectListItem("", ""));
            if (EventId != Guid.Empty)
                ViewModel.EventItems.Add(new SelectListItem("", EventId.ToString()));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditTableViewModel, CreateUpdateTableDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}