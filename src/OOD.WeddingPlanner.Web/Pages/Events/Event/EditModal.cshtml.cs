using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.Events;
using OOD.WeddingPlanner.Events.Dtos;
using OOD.WeddingPlanner.Web.Pages.Events.Event.ViewModels;

namespace OOD.WeddingPlanner.Web.Pages.Events.Event
{
    public class EditModalModel : WeddingPlannerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditEventViewModel ViewModel { get; set; }

        private readonly IEventAppService _service;

        public EditModalModel(IEventAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<EventDto, CreateEditEventViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditEventViewModel, CreateUpdateEventDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}