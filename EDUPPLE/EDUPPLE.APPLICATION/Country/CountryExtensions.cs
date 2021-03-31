using EDUPPLE.APPLICATION.Common;
using EDUPPLE.APPLICATION.Country.Models;
using EDUPPLE.DOMAIN.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace EDUPPLE.APPLICATION.Country
{
    public static class CountryExtensions
    {
        public static IServiceCollection CountryDomainHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddEntityCommands<IUnitOfWork, DOMAIN.Entities.Country, int, CountryReadModel, CountryCreateModel, CountryUpdateModel>();
            return serviceCollection;
        }
    }
}
