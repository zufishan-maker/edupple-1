using EDUPPLE.APPLICATION.City.Models;
using EDUPPLE.APPLICATION.Common;
using EDUPPLE.DOMAIN.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace EDUPPLE.APPLICATION.City
{
    public static class CityExtensions
    {
        public static IServiceCollection CityDomainHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddEntityCommands<IUnitOfWork, DOMAIN.Entities.City, int, CityReadModel, CityCreateModel, CityUpdateModel>();
            return serviceCollection;
        }       
    }
}
