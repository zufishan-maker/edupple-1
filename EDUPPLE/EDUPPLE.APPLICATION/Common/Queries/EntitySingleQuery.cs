using EDUPPLE.INFRASTRUCTURE.Extensions;
using System.Security.Principal;

namespace EDUPPLE.APPLICATION.Common.Queries
{


    public class EntitySingleQuery<TEntity, TReadModel> : PrincipalQueryBase<TReadModel>
       where TEntity : class
    {
        public EntitySingleQuery(SingleQuery<TEntity> query):base(null)
        {
            Query = query;
        }
        public EntitySingleQuery(SingleQuery<TEntity> query, ICurrentUser principal) : base(principal)
        {
            Query = query;
            Principal = principal;
          
        }
        public SingleQuery<TEntity> Query { get; set; }
      

    }
}
