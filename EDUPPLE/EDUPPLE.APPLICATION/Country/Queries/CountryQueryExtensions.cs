using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Country.Queries
{
    
    public static class CountryQueryExtensions
    {
        public static bool IsCountryExist(this IQueryable<DOMAIN.Entities.Country> dbSet, string name)
            => dbSet.Where(x => x.Name.Trim().ToUpper().Equals(name.Trim().ToUpper())).Any();

        public static bool IsCountryByIdExist(this IQueryable<DOMAIN.Entities.Country> dbSet, int id)
           => dbSet.Where(x => x.Id == id).Any();
    }
}
