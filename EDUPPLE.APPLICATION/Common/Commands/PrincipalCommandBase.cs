using EDUPPLE.INFRASTRUCTURE.Extensions;
using MediatR;

namespace EDUPPLE.APPLICATION.Common.Commands
{
    public abstract class PrincipalCommandBase<TResponse> : IRequest<TResponse>
    {
        protected PrincipalCommandBase(ICurrentUser principal)
        {
            Principal = principal;
        }
        public ICurrentUser Principal { get; }
    }
}
