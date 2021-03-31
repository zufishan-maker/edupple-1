using AutoMapper;
using EDUPPLE.APPLICATION.Authentication.Commands;
using EDUPPLE.APPLICATION.Authentication.Models;
using EDUPPLE.APPLICATION.Common.Handlers;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.DOMAIN.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Authentication.Handlers
{
    public class ResetPasswordCommandHandler<TUnitOfWork> : DataContextHandlerBase<TUnitOfWork, ResetPasswordCommand, EntityResponseModel<TokenResetModel>>
          where TUnitOfWork : IUnitOfWork
    {
        private UserManager<DOMAIN.Entities.User> _userManager;

        public ResetPasswordCommandHandler(ILoggerFactory loggerFactory,
         TUnitOfWork dataContext,
         UserManager<DOMAIN.Entities.User> userManager,
         IMapper mapper
         ) : base(loggerFactory, dataContext, mapper)
        {
            _userManager = userManager;
        }

        protected override async Task<EntityResponseModel<TokenResetModel>> ProcessAsync(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = new EntityResponseModel<TokenResetModel>();
            var user = await _userManager.FindByEmailAsync(request.Model.UserName).ConfigureAwait(false);
            if (user.OTP.Equals(int.Parse(request.Model.Token)))
            {
                
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user,request.Model.Password);
                await _userManager.UpdateAsync(user).ConfigureAwait(false);
                response.Data = request.Model;
                response.StatusCode = StatusCodes.Status200OK;
                response.ReturnMessage.Add("Successfully change your password.,");
            }
            else
            {
                response.Data =default;
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.ReturnMessage.Add("Unable to change your password.,");
            }
            return response;
        }
    }
}
