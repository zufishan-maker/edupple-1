using EDUPPLE.APPLICATION.Country.Models;
using EDUPPLE.DOMAIN.Interface;
using EDUPPLE.INFRASTRUCTURE.FluentValidation;
using FluentValidation;
using FluentValidation.Results;

namespace EDUPPLE.APPLICATION.Country.Validations
{
    public class CountryUpdateCommandValidation : BaseValidator<CountryUpdateModel, DOMAIN.Entities.Country>
    {
        public CountryUpdateCommandValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Country Name is empty.");
        }
    }
}
