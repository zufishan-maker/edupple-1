
using EDUPPLE.APPLICATION.Authentication.Models;
using EDUPPLE.APPLICATION.Common.Commands;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.INFRASTRUCTURE.Extensions;

namespace EDUPPLE.APPLICATION.Authentication.Commands
{
    public class ResetPasswordCommand :
        EntityModelCommand<TokenResetModel, EntityResponseModel<TokenResetModel>>        
    {
        public ResetPasswordCommand(ICurrentUser principal, TokenResetModel model) : base(principal, model)
        {
           
        }
    }
}
