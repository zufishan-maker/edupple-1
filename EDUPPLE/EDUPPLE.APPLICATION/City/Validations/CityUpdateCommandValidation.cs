using EDUPPLE.APPLICATION.City.Models;
using EDUPPLE.APPLICATION.Country.Queries;
using EDUPPLE.DOMAIN.Interface;
using EDUPPLE.INFRASTRUCTURE.FluentValidation;
using FluentValidation;
using FluentValidation.Results;

namespace EDUPPLE.APPLICATION.Country.Validations
{
    public class CityUpdateCommandValidation : BaseValidator<CityUpdateModel, DOMAIN.Entities.City>
    {
        public CityUpdateCommandValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _ = RuleFor(p => p).Custom((p, context) =>
            {
                if (string.IsNullOrWhiteSpace(p.Name))
                {
                    context.AddFailure(new ValidationFailure($"Name", $"City Name is required."));
                }
            });

            When(p => p.CountryId.HasValue, () =>
            {
                _ = RuleFor(p => p).Custom((p, context) =>
                {
                    if (!DataContext.Set<DOMAIN.Entities.Country>().IsCountryByIdExist(p.CountryId.Value))
                    {
                        context.AddFailure(new ValidationFailure($"CountryId", $"CountryId is required."));
                    }
                });
            }).Otherwise(() =>
            {
                _ = RuleFor(p => p.CountryId).Custom((x, context) =>
                {
                    context.AddFailure(new ValidationFailure($"CountryId", $"CountryId is null."));
                });
            });

           
        }
    }
}
