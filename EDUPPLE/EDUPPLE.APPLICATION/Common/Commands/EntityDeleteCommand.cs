using EDUPPLE.INFRASTRUCTURE.Extensions;
using System.Security.Principal;

namespace EDUPPLE.APPLICATION.Common.Commands
{

    public class EntityDeleteCommand<TKey, TReadModel>
      : EntityIdentifierCommand<TKey, TReadModel>
    {
        public EntityDeleteCommand(ICurrentUser principal, TKey id, bool isTrans) : base(principal, id)
        {
            IsTrans = isTrans;
        }
        public EntityDeleteCommand(ICurrentUser principal, TKey id, string includeProperties, bool isTrans) : base(principal, id)
        {
            IncludeProperties = includeProperties;
            IsTrans = isTrans;
        }
        public sealed override string IncludeProperties { get; set; }
        public bool IsTrans { get; set; }

    }
}
