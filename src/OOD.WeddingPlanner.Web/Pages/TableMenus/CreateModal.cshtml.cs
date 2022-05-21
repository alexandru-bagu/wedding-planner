using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OOD.WeddingPlanner.TableMenus;
using OOD.WeddingPlanner.TableMenus.Dtos;
using OOD.WeddingPlanner.Web.Pages.TableMenus.ViewModels;

namespace OOD.WeddingPlanner.Web.Pages.TableMenus
{
    public class CreateModalModel : WeddingPlannerPageModel
    {
        [BindProperty]
        public CreateEditTableMenuViewModel ViewModel { get; set; }

        private readonly ITableMenuAppService _service;

        public CreateModalModel(ITableMenuAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditTableMenuViewModel, CreateUpdateTableMenuDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}