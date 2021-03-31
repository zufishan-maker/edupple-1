using EDUPPLE.INFRASTRUCTURE.Extensions;
using System.Security.Principal;

namespace EDUPPLE.APPLICATION.Common.Queries
{
    public class EntityListQuery<TEntity, TReadModel> : PrincipalQueryBase<TReadModel>
       where TEntity : class
    {
        public EntityListQuery(EntityQuery<TEntity> query) : this((ICurrentUser)null)
        {
            Query = query;
        }
        public EntityListQuery(ICurrentUser principal) : base(principal)
        {           
            Principal = principal;
        }
        public EntityListQuery(EntityQuery<TEntity> query, ICurrentUser principal):base(principal)
        {
            Query = query;
            Principal = principal;
        }      
        public EntityQuery<TEntity> Query { get; set; }
     
    }
}
