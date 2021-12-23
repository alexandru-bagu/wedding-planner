using OOD.WeddingPlanner.Locations.Dtos;
using OOD.WeddingPlanner.Web.Pages.Locations.Location.ViewModels;
using AutoMapper;

namespace OOD.WeddingPlanner.Web
{
    public class WeddingPlannerWebAutoMapperProfile : Profile
    {
        public WeddingPlannerWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.
            CreateMap<LocationDto, CreateEditLocationViewModel>();
            CreateMap<CreateEditLocationViewModel, CreateUpdateLocationDto>();
        }
    }
}
