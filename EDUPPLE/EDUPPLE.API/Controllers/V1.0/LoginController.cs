using EDUPPLE.API.Helper;
using EDUPPLE.APPLICATION.Authentication.Commands;
using EDUPPLE.APPLICATION.Authentication.Models;
using EDUPPLE.APPLICATION.Authentication.User.Models;
using EDUPPLE.APPLICATION.Common.Commands;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.APPLICATION.Helper;
using EDUPPLE.INFRASTRUCTURE.Extensions;
using EDUPPLE.INFRASTRUCTURE.Swagger;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.API.Controllers
{
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class LoginController : BaseController
    {
        public LoginController(IMediator mediator,ICurrentUser currentUser)
            : base(mediator, currentUser)
        {
           
        }
        [AllowAnonymous]
        [HttpPost("SignIn")]
        [System.Obsolete]
        public async Task<IActionResult> SignIn(TokenRequestModel model, CancellationToken cancellationToken)
        {
           
            var command = new AuthenticateCommand(Request.UserAgent(), model);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<TokenResponseModel>.GenerateResponse(result);
        }

        [AllowAnonymous]
        [HttpPost("ForgetPassword")]
        [ProducesResponseType(typeof(EntityResponseModel<bool>), 200)]
        [System.Obsolete]
        public async Task<IActionResult> ForgotPassword(CancellationToken cancellationToken, UserForgotPasswordModel model)
        {
            var command = new ForgotPasswordCommand(Request.UserAgent(), model);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<bool>.GenerateResponse(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Reset-Password")]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseModel<TokenResetModel>), 200)]
       
        public async Task<IActionResult> ResetPasswordVerification(CancellationToken cancellationToken, TokenResetModel model)
        {
            var command = new ResetPasswordCommand(CurrentUser,model);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<TokenResetModel>.GenerateResponse(result);
        }

    }
}
