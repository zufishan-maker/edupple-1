using EDUPPLE.APPLICATION.City.Models;
using EDUPPLE.APPLICATION.City.Queries;
using EDUPPLE.APPLICATION.Country.Queries;
using EDUPPLE.DOMAIN.Interface;
using EDUPPLE.INFRASTRUCTURE.FluentValidation;
using FluentValidation;
using FluentValidation.Results;

namespace EDUPPLE.APPLICATION.City.Validations
{


    public class CityInsertCommandValidation : BaseValidator<CityCreateModel, DOMAIN.Entities.City>
    {
        public CityInsertCommandValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {           
            When(p => !string.IsNullOrWhiteSpace(p.Name), () =>
            {
                _ = RuleFor(p => p).Custom((p, context) =>
                {
                    if (_Entities.IsCityExist(p.Name))
                    {
                        context.AddFailure(new ValidationFailure($"Name", $"City is already exists."));
                    }
                });                
            }).Otherwise(() =>
            {
                _ = RuleFor(p => p.Name).Custom((x, context) =>
                {
                    context.AddFailure(new ValidationFailure($"Name", $"City Name is Empty.,"));
                });
            });

            When(p => p.CountryId.HasValue, ()=> {
                _ = RuleFor(p => p).Custom((p, context) =>
                {
                    if(!DataContext.Set<DOMAIN.Entities.Country>().IsCountryByIdExist(p.CountryId.Value)) 
                        context.AddFailure(new ValidationFailure($"CountryId", $"Country is not exists."));
                });
            
            }).Otherwise(() =>
            {
                _ = RuleFor(p => p.CountryId).Custom((x, context) =>
                {
                    context.AddFailure(new ValidationFailure($"CountryId", $"CountryId is null.,"));
                });
            });
        }

    }
}
