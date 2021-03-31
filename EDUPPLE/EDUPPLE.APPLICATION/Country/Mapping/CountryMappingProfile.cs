using EDUPPLE.APPLICATION.Country.Models;

namespace EDUPPLE.APPLICATION.Country.Mapping
{
    public class CountryMappingProfile : AutoMapper.Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<CountryCreateModel, DOMAIN.Entities.Country>();
            CreateMap<CountryUpdateModel, DOMAIN.Entities.Country>();
            CreateMap<DOMAIN.Entities.Country, CountryReadModel>();
        }
    }
}
