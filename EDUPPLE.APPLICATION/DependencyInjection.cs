using EDUPPLE.APPLICATION.Authentication;
using EDUPPLE.APPLICATION.City;
using EDUPPLE.APPLICATION.Country;
using EDUPPLE.APPLICATION.EmailTemplate;
using EDUPPLE.APPLICATION.EmailTemplate.EmailService.Interface;
using EDUPPLE.APPLICATION.EmailTemplate.EmailService.Service;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IEmailTemplateService), typeof(EmailTemplateService));
            services.AddScoped(typeof(IEmailDeliveryService), typeof(EmailDeliveryService));
            services.AddMassTransitConsumer();
            //handlers
            //Authentication
            services.AuthenticationDomainHandlers();
            services.CountryDomainHandlers();
            services.CityDomainHandlers();
            return services;
        }
    }
}
