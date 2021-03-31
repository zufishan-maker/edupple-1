using EDUPPLE.APPLICATION.Authentication.Models;
using EDUPPLE.APPLICATION.Authentication.User.Models;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.INFRASTRUCTURE.Model;
using MediatR;

namespace EDUPPLE.APPLICATION.Authentication.Commands
{
    public class ForgotPasswordCommand : IRequest<EntityResponseModel<bool>>
    {
        public ForgotPasswordCommand(UserAgentModel userAgent, UserForgotPasswordModel model)
        {
            UserAgent = userAgent;
            Model = model;
        }

        public UserForgotPasswordModel Model { get; set; }

        public UserAgentModel UserAgent { get; set; }
    }
}
