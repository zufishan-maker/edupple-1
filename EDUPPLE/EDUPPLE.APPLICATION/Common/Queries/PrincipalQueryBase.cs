using EDUPPLE.INFRASTRUCTURE.Extensions;
using MediatR;
using System.Security.Principal;

namespace EDUPPLE.APPLICATION.Common.Queries
{
    public abstract class PrincipalQueryBase<TResponse> : IRequest<TResponse>
    {
        protected PrincipalQueryBase(ICurrentUser principal)
        {
            Principal = principal;
        }
        public ICurrentUser Principal { get; set; }
    }
}
