using EDUPPLE.APPLICATION.Authentication.User.Models;
using EDUPPLE.APPLICATION.EmailTemplate.EmailService.Interface;
using EDUPPLE.DOMAIN.Entities;
using EDUPPLE.DOMAIN.Interface;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.EmailTemplate.Consumer
{
    public class ForgetPasswordConsumer : IConsumer<UserForgotPasswordEmail>
    {
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IUnitOfWork _unitOfWork;
        public ForgetPasswordConsumer(IEmailTemplateService emailTemplateService,
            IUnitOfWork unitOfWork)
        {
            _emailTemplateService = emailTemplateService;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<UserForgotPasswordEmail> context)
        {
            var message = _emailTemplateService.SendForgotPassword(context.Message).Result;
            await _emailTemplateService.SendMessage(message).ConfigureAwait(false);           
        }
    }
}
