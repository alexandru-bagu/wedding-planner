using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.TableMenus;
using OOD.WeddingPlanner.TableMenus.Dtos;
using OOD.WeddingPlanner.Web.Pages.TableMenus.ViewModels;

namespace OOD.WeddingPlanner.Web.Pages.TableMenus
{
    public class EditModalModel : WeddingPlannerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditTableMenuViewModel ViewModel { get; set; }

        private readonly ITableMenuAppService _service;

        public EditModalModel(ITableMenuAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<TableMenuDto, CreateEditTableMenuViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditTableMenuViewModel, CreateUpdateTableMenuDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}