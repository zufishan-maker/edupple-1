using EDUPPLE.APPLICATION.EmailTemplate.Consumer;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace EDUPPLE.APPLICATION.EmailTemplate
{
    public static class EmailServiceExtensions
    {
        public static IServiceCollection AddMassTransitConsumer(this IServiceCollection serviceCollection)
        {           
            serviceCollection.AddMassTransit(x =>
            {
                x.AddConsumer<ForgetPasswordConsumer>();
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ReceiveEndpoint(typeof(ForgetPasswordConsumer).Name, e =>
                    {
                        e.ConfigureConsumer<ForgetPasswordConsumer>(context);
                    });
                });
            });

            serviceCollection.AddMassTransitHostedService();
            return serviceCollection;
        }
    }
}
