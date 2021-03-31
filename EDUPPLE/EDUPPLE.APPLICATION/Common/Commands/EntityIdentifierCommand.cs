using EDUPPLE.INFRASTRUCTURE.Extensions;
using System.Security.Principal;

namespace EDUPPLE.APPLICATION.Common.Commands
{
    public abstract class EntityIdentifierCommand<TKey, TReadModel>
          : PrincipalCommandBase<TReadModel>
    {
        public abstract string IncludeProperties { get; set; }
        protected EntityIdentifierCommand(ICurrentUser principal, TKey id)
            : base(principal)
        {
            Id = id;
        }
        public TKey Id { get; }
    }
}
