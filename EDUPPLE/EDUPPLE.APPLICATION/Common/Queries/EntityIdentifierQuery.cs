using EDUPPLE.INFRASTRUCTURE.Extensions;
using System.Security.Principal;

namespace EDUPPLE.APPLICATION.Common.Queries
{
    public class EntityIdentifierQuery<TKey, TReadModel> : PrincipalQueryBase<TReadModel>
    {
        public EntityIdentifierQuery(ICurrentUser principal, TKey id)
            : base(principal)
        {
            Id = id;           
        }
        public EntityIdentifierQuery(ICurrentUser principal, TKey id, string includeProperties)
           : base(principal)
        {
            Id = id;
            IncludeProperties = includeProperties;          
        }
        public TKey Id { get; }
        public string IncludeProperties { get; set; }      
    }
}
