using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.RefreshToken.Extensions
{
    public static class RefreshTokenExtensions
    {
        public static Task<DOMAIN.Entities.RefreshToken> GetByTokenHashedAsync(this IQueryable<DOMAIN.Entities.RefreshToken> queryable, string tokenHashed)
        {
            return queryable.FirstOrDefaultAsync(r => r.TokenHashed == tokenHashed);
        }
    }
}
