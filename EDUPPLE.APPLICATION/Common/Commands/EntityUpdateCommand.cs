using EDUPPLE.INFRASTRUCTURE.Extensions;
using System.Security.Principal;

namespace EDUPPLE.APPLICATION.Common.Commands
{
    public class EntityUpdateCommand<TKey, TModel, TReadModel>
        : EntityModelCommand<TModel, TReadModel>
    {
        public EntityUpdateCommand(ICurrentUser principal, TKey id, TModel model, bool isTrans) : base(principal, model)
        {
            Id = id;
            IsTrans = isTrans;
        }

        public EntityUpdateCommand(ICurrentUser principal, TModel model, TKey id, string childEntities, bool isTrans) : base(principal, model)
        {           

            ChildEntities = childEntities;
            Id = id;
            IsTrans = isTrans;
        }
        public TKey Id { get; }
        public string ChildEntities { get; set; } 
        public bool IsTrans { get; set; }
    }
}
