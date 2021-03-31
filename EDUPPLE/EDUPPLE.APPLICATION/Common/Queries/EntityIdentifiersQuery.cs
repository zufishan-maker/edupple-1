using EDUPPLE.INFRASTRUCTURE.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace EDUPPLE.APPLICATION.Common.Queries
{
    public class EntityIdentifiersQuery<TKey, TReadModel> : PrincipalQueryBase<TReadModel>
    {
        public EntityIdentifiersQuery(ICurrentUser principal, IEnumerable<TKey> ids)
            : base(principal)
        {
            Ids = ids.ToList();
        }

        public IReadOnlyCollection<TKey> Ids { get; }
    }
}
