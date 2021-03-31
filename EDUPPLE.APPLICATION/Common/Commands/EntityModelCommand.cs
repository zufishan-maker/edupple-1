using EDUPPLE.INFRASTRUCTURE.Extensions;
using System;
using System.Security.Principal;

namespace EDUPPLE.APPLICATION.Common.Commands
{
    public abstract class EntityModelCommand<TEntityModel, TReadModel> : PrincipalCommandBase<TReadModel>
    {
        protected EntityModelCommand(ICurrentUser principal, TEntityModel model) : base(principal)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            Model = model;

        }        
        public TEntityModel Model { get; set; }
    }
}
