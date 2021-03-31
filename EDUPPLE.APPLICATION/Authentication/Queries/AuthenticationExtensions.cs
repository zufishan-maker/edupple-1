using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Authentication.Queries
{
   public static class AuthenticationExtensions
    {
        public static bool IsEmailExist(this IQueryable<DOMAIN.Entities.User> dbSet, string email) 
            => dbSet.Where(x => x.Email.Trim().ToUpper().Equals(email.Trim().ToUpper())).Any();        
    }
}
