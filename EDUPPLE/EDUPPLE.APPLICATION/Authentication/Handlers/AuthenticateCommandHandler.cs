using AutoMapper;
using EDUPPLE.APPLICATION.Authentication.Commands;
using EDUPPLE.APPLICATION.Authentication.Extensions;
using EDUPPLE.APPLICATION.Authentication.Helper;
using EDUPPLE.APPLICATION.Authentication.Models;
using EDUPPLE.APPLICATION.Common.Handlers;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.APPLICATION.RefreshToken.Extensions;
using EDUPPLE.DOMAIN.Entities;
using EDUPPLE.DOMAIN.Interface;
using EDUPPLE.INFRASTRUCTURE.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Authentication.Handlers
{
    public class AuthenticateCommandHandler<TUnitOfWork> : DataContextHandlerBase<TUnitOfWork, AuthenticateCommand, EntityResponseModel<TokenResponseModel>>
         where TUnitOfWork : IUnitOfWork
    {
        private UserManager<DOMAIN.Entities.User> UserManager;
        private readonly IOptions<PrincipalConfiguration> _principalOptions;
        private readonly IOptions<HostingConfiguration> _hostingOptions;

        public AuthenticateCommandHandler(ILoggerFactory loggerFactory, TUnitOfWork dataContext,
           IMapper mapper, UserManager<DOMAIN.Entities.User> userManager, IOptions<PrincipalConfiguration> principalOptions,
           IOptions<HostingConfiguration> hostingOptions)
            : base(loggerFactory, dataContext, mapper)
        {
            UserManager = userManager;
            _principalOptions = principalOptions;
            _hostingOptions = hostingOptions;
        }

        /// <summary>
        /// Processes the asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected override async Task<EntityResponseModel<TokenResponseModel>> ProcessAsync(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var tokenRequest = request.TokenRequest;

            return tokenRequest.GrantType switch
            {
                TokenConstants.GrantTypes.Password => await PasswordAuthenticate(request, cancellationToken).ConfigureAwait(false),
                TokenConstants.GrantTypes.RefreshToken => await RefreshAuthenticate(request).ConfigureAwait(false),
                _ => new EntityResponseModel<TokenResponseModel>()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    ReturnStatus = false,
                    ReturnMessage = new List<string>() { AuthenticationMessage.InvalidGrantType }
                }
            };         
        }

        /// <summary>
        /// Passwords the authenticate.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        private async Task<EntityResponseModel<TokenResponseModel>> PasswordAuthenticate(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var response = new EntityResponseModel<TokenResponseModel>();
            var tokenRequest = request.TokenRequest;           
            var userName = tokenRequest.UserName;
            var password = tokenRequest.Password;

            var User = await UserManager.FindByEmailAsync(userName).ConfigureAwait(false);

            if (User == null)
            {
                Logger.LogWarning(string.Format(AuthenticationMessage.InvalidCredential,userName));
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.ReturnStatus = false;
                response.ReturnMessage = new List<string>() { string.Format(AuthenticationMessage.InvalidCredential, userName) };
                return response;
            }            

            if (User.LockoutEnabled && User.LockoutEnd >= DateTimeOffset.UtcNow)
            {
                Logger.LogWarning(string.Format(AuthenticationMessage.UserLockOut,userName));
                return new EntityResponseModel<TokenResponseModel>()
                {
                    StatusCode = StatusCodes.Status423Locked,
                    ReturnStatus = false,
                    ReturnMessage = new List<string>() { string.Format(AuthenticationMessage.UserLockOut, userName) }
                };
            }

            var IsAuthenticate = await UserManager.CheckPasswordAsync(User, password).ConfigureAwait(false);
            if(!IsAuthenticate)
            {                        
                if (UserManager.SupportsUserLockout && User.LockoutEnabled) await UserManager.AccessFailedAsync(User).ConfigureAwait(false);
                if (User.AccessFailedCount > 5)
                {
                    Logger.LogWarning(string.Format(AuthenticationMessage.UserLockOut, userName));
                    User.LockoutEnabled = true;
                    User.LockoutEnd = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(5));
                }
            }
            
            if (User != null && IsAuthenticate)
            {
                if (UserManager.SupportsUserLockout && await UserManager.GetAccessFailedCountAsync(User) > 0)
                {
                   await UserManager.ResetAccessFailedCountAsync(User).ConfigureAwait(false);
                }
                var userRoles = await UserManager.GetRolesAsync(User).ConfigureAwait(false);
                var accessToken = TokenExtensions.CreateToken(_principalOptions, _hostingOptions,User,userRoles);
                var refreshToken = await CreateRefreshToken(userName, User.Id).ConfigureAwait(false);
                return new EntityResponseModel<TokenResponseModel>()
                {
                    StatusCode = StatusCodes.Status200OK,
                    ReturnStatus = true,
                    Data = new TokenResponseModel
                    {
                        AccessToken = accessToken,
                        ExpiresIn = (int)_principalOptions.Value.TokenExpire.TotalSeconds,
                        RefreshToken = refreshToken,
                    },
                    ReturnMessage = new List<string>() { AuthenticationMessage.Login },
                };
            }
            return response;

        }
        /// <summary>
        /// Refreshes the authenticate.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private async Task<EntityResponseModel<TokenResponseModel>> RefreshAuthenticate(AuthenticateCommand request)
        {
            var tokenRequest = request.TokenRequest;
            var token = tokenRequest.RefreshToken;
            var hashedToken = TokenExtensions.HashToken(token);
            var refreshToken = await DataContext.Set<DOMAIN.Entities.RefreshToken>().Include(x=>x.User)
               .GetByTokenHashedAsync(hashedToken)
               .ConfigureAwait(false);

            if (refreshToken == null)
            {
                return new EntityResponseModel<TokenResponseModel>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ReturnStatus = false,
                    ReturnMessage = new List<string>() { AuthenticationMessage.RefreshTokenNotFound },
                };
            }

            if (DateTimeOffset.UtcNow > refreshToken.Expires)
            {
                Logger.LogWarning(AuthenticationMessage.RefreshTokenExpired);
                return new EntityResponseModel<TokenResponseModel>()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ReturnStatus = false,
                    ReturnMessage = new List<string>() {AuthenticationMessage.RefreshTokenExpired },
                };
            }


            var userRoles = await UserManager.GetRolesAsync(refreshToken.User).ConfigureAwait(false);
            var accessToken = TokenExtensions.CreateToken(_principalOptions, _hostingOptions, refreshToken.User, userRoles);
            var refreshNewToken = await CreateRefreshToken(refreshToken.UserName, refreshToken.UserId).ConfigureAwait(false);
            
            // delete refresh token to prevent reuse
            DataContext.Set<DOMAIN.Entities.RefreshToken>().Remove(refreshToken);
            await DataContext.SaveChangesNoConflicAsync(default).ConfigureAwait(false);
            return new EntityResponseModel<TokenResponseModel>()
            {
                StatusCode = StatusCodes.Status200OK,
                ReturnStatus = true,
                Data = new TokenResponseModel
                {
                    AccessToken = accessToken,
                    ExpiresIn = (int)_principalOptions.Value.TokenExpire.TotalSeconds,
                    RefreshToken = refreshNewToken,
                },
                ReturnMessage = new List<string>() { AuthenticationMessage.Login },
            };
        }
        /// <summary>
        /// Creates the refresh token.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private async Task<string> CreateRefreshToken(string userName, string userId)
        {
            var refreshToken = Guid.NewGuid().ToString("n");
            Logger.LogDebug(string.Format(format: AuthenticationMessage.RefreshToken,userName));
           
            var hashedToken = TokenExtensions.HashToken(refreshToken);
            var token = new DOMAIN.Entities.RefreshToken
            {
                TokenHashed = hashedToken,               
                UserId = userId,
                UserName = userName,
                Issued = DateTimeOffset.UtcNow,
                Expires = DateTimeOffset.UtcNow.Add(_principalOptions.Value.RefreshExpire),
            };

            await DataContext.Set<DOMAIN.Entities.RefreshToken>().AddAsync(token).ConfigureAwait(false);
            await DataContext.SaveChangesNoConflicAsync(cancellationToken: default).ConfigureAwait(false);
            return refreshToken;
        }

    }
}
