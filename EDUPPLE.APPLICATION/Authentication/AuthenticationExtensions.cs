using EDUPPLE.APPLICATION.Authentication.Commands;
using EDUPPLE.APPLICATION.Authentication.Handlers;
using EDUPPLE.APPLICATION.Authentication.Models;
using EDUPPLE.APPLICATION.Common.Behaviours;
using EDUPPLE.APPLICATION.Common.Commands;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.DOMAIN.Interface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EDUPPLE.APPLICATION.Authentication
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AuthenticationDomainHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRequestHandler<AuthenticateCommand, EntityResponseModel<TokenResponseModel>>, AuthenticateCommandHandler<IUnitOfWork>>();
            serviceCollection.AddTransient<IRequestHandler<ForgotPasswordCommand, EntityResponseModel<bool>>, UserForgotPasswordCommandHandler<IUnitOfWork>>();
            serviceCollection.AddTransient<IRequestHandler<ResetPasswordCommand, EntityResponseModel<TokenResetModel>>, ResetPasswordCommandHandler<IUnitOfWork>>();
            serviceCollection.AddTransient<IPipelineBehavior<ResetPasswordCommand, EntityResponseModel<TokenResetModel>>, ValidateEntityModelCommandBehavior<TokenResetModel, TokenResetModel>>();

            return serviceCollection;
        }
        }
}
