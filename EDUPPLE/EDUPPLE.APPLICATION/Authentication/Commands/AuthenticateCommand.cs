using EDUPPLE.APPLICATION.Authentication.Models;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.INFRASTRUCTURE.Model;
using MediatR;

namespace EDUPPLE.APPLICATION.Authentication.Commands
{
    public class AuthenticateCommand : IRequest<EntityResponseModel<TokenResponseModel>>
    {
        public AuthenticateCommand(UserAgentModel userAgent, TokenRequestModel tokenRequest)
        {
            UserAgent = userAgent;
            TokenRequest = tokenRequest;
        }

        public TokenRequestModel TokenRequest { get; set; }

        public UserAgentModel UserAgent { get; set; }
    }
}
