using EDUPPLE.APPLICATION.City.Models;

namespace EDUPPLE.APPLICATION.City.Mapping
{
    public class CityMappingProfile : AutoMapper.Profile
    {
        public CityMappingProfile()
        {
            CreateMap<CityCreateModel, DOMAIN.Entities.City>();
            CreateMap<CityUpdateModel, DOMAIN.Entities.City>();
            CreateMap<DOMAIN.Entities.City, CityReadModel>();
        }
    }
}
