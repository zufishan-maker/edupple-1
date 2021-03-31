using AutoMapper;
using EDUPPLE.APPLICATION.Authentication.Commands;
using EDUPPLE.APPLICATION.Authentication.Helper;
using EDUPPLE.APPLICATION.Authentication.User.Models;
using EDUPPLE.APPLICATION.Common.Commands;
using EDUPPLE.APPLICATION.Common.Handlers;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.DOMAIN.Interface;
using EDUPPLE.INFRASTRUCTURE.Extensions;
using EDUPPLE.INFRASTRUCTURE.Options;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Authentication.Handlers
{   
    public class UserForgotPasswordCommandHandler<TUnitOfWork>
   : DataContextHandlerBase<TUnitOfWork, ForgotPasswordCommand, EntityResponseModel<bool>>
   where TUnitOfWork : IUnitOfWork
    {       
        private readonly IOptions<HostingConfiguration> _hostingOptions;
        private readonly UserManager<DOMAIN.Entities.User> _userManager;
        private const string Api = "api/Login/";
        private readonly IBusControl _message;
       
        public UserForgotPasswordCommandHandler(ILoggerFactory loggerFactory, TUnitOfWork dataContext,
          UserManager<DOMAIN.Entities.User> userManager,
          IMapper mapper,
          IOptions<HostingConfiguration> hostingOptions, ICurrentUser userContext,
          IBusControl message) : base(loggerFactory, dataContext, mapper)
        {

            _hostingOptions = hostingOptions;
            _userManager = userManager;
            _message = message;
        }

        protected override async Task<EntityResponseModel<bool>> ProcessAsync(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = new EntityResponseModel<bool>();
            try
            {
                Logger.LogInformation($"{ForgotPasswordMessage.ForgotPasswordProcessing}{request.Model}");

                var user = await _userManager.FindByEmailAsync(request.Model.EmailAddress).ConfigureAwait(false);
                if (user == null) return new EntityResponseModel<bool>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ReturnStatus = false,
                    ReturnMessage = new List<string>() { AuthenticationMessage.User_Not_Found },
                    Data = false
                };
              
                user.OTP = int.Parse(new Random(Guid.NewGuid().GetHashCode()).Next(0, 9999).ToString("D4"));

                Logger.LogInformation($"{ForgotPasswordMessage.SuccessfullyGenerateToken} { user.OTP.ToString()}");

                await _message.Publish<UserForgotPasswordEmail>(new
                {
                    ResetToken = user.OTP.ToString(),
                    Link = $"{ user.OTP.ToString()}",
                    DisplayName = user.NormalizedUserName,
                    EmailAddress = user.Email
                }, cancellationToken);
                await _userManager.UpdateAsync(user).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                Logger.LogError($"{ForgotPasswordMessage.ErrorForgotPassword} {ex.Message}");
            }
            response.Data = default;
            response.ReturnMessage.Add($"{ForgotPasswordMessage.SuccessFully}");
            response.StatusCode = StatusCodes.Status200OK;
            return response;
        }        
    }
}
