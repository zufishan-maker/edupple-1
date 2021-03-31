using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.City.Queries
{
    public static class CityQueryExtensions
    {
        public static bool IsCityExist(this IQueryable<DOMAIN.Entities.City> dbSet, string name)
          => dbSet.Where(x => x.Name.Trim().ToUpper().Equals(name.Trim().ToUpper())).Any();
    }
}
