using EDUPPLE.INFRASTRUCTURE.Extensions;
using System.Security.Principal;

namespace EDUPPLE.APPLICATION.Common.Commands
{
    public class EntityCustomModelCommand<TEntityModel, TReadModel>
    : EntityModelCommand<TEntityModel, TReadModel>
    {
        public EntityCustomModelCommand(ICurrentUser principal, TEntityModel model) : base(principal, model)
        {
            Modal = model;
        }
        public TEntityModel Modal { get; }
    }
}
