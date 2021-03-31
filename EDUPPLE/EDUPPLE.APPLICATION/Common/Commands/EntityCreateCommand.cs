using EDUPPLE.INFRASTRUCTURE.Extensions;
using System.Security.Principal;

namespace EDUPPLE.APPLICATION.Common.Commands
{
    public class EntityCreateCommand<TModel, TReadModel>
          : EntityModelCommand<TModel, TReadModel>
    {
        public EntityCreateCommand(ICurrentUser principal, TModel model, bool isTrans) : base(principal, model)
        {
            IsTrans = isTrans;
        }

        public EntityCreateCommand(ICurrentUser principal, TModel model, string childEntities, bool isTrans) : base(principal, model)
        {  
            ChildEntities = childEntities;
            IsTrans = isTrans;

        }
        public string ChildEntities { get; set; }
        public bool IsTrans { get; set; }
    }
}
