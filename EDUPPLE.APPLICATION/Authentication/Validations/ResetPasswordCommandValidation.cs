using EDUPPLE.APPLICATION.Authentication.Models;
using EDUPPLE.APPLICATION.Authentication.Queries;
using EDUPPLE.DOMAIN.Interface;
using EDUPPLE.INFRASTRUCTURE.FluentValidation;
using FluentValidation;
using FluentValidation.Results;

namespace EDUPPLE.APPLICATION.Authentication.Validations
{
    public class ResetPasswordCommandValidation : BaseValidator<TokenResetModel,DOMAIN.Entities.User>
    {       
        public ResetPasswordCommandValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

            RuleFor(p => p.Token).NotEmpty().WithMessage("Token is Empty.,");
            When(p => !string.IsNullOrWhiteSpace(p.UserName), () =>
            {
                _ = RuleFor(p => p).Custom((p, context) =>
                {
                    if (!_Entities.IsEmailExist(p.UserName))
                    {
                        context.AddFailure(new ValidationFailure($"User Name", $"Invalid User Name or User Does not Exists.,"));
                    }                   
                });
                _ = RuleFor(p => p.UserName).EmailAddress().WithMessage("Invalid Email Format.,");
            }).Otherwise(() =>
            {
                _ = RuleFor(p => p.UserName).Custom((x, context) =>
                {
                    context.AddFailure(new ValidationFailure($"User Name", $" User Name is Empty.,"));
                });               
            });

            When(p => (!string.IsNullOrWhiteSpace(p.Password) && !string.IsNullOrWhiteSpace(p.ConfirmPassword)), () =>
            {
                _ = RuleFor(p => p).Custom((p, context) =>
                {
                    if (!p.Password.Equals(p.ConfirmPassword))
                    {
                        context.AddFailure(new ValidationFailure($"Password", $"Password and Confirm Password is not equal"));
                    }
                });               
            }).Otherwise(() =>
            {
                _ = RuleFor(p => new { p.Password, p.ConfirmPassword }).Custom((x, context) =>
                {
                    if(string.IsNullOrWhiteSpace(x.ConfirmPassword)) context.AddFailure(new ValidationFailure($"Confirm Password", $"Confirm Password is Empty.,"));
                    if (string.IsNullOrWhiteSpace(x.Password)) context.AddFailure(new ValidationFailure($"Password", $"Password is Empty.,"));
                });
            });
        }
   
    }
}
