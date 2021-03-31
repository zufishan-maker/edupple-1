using EDUPPLE.APPLICATION.Country.Models;
using EDUPPLE.APPLICATION.Country.Queries;
using EDUPPLE.DOMAIN.Interface;
using EDUPPLE.INFRASTRUCTURE.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Country.Validations
{
   

    public class CountryInsertCommandValidation : BaseValidator<CountryCreateModel, DOMAIN.Entities.Country>
    {
        public CountryInsertCommandValidation(IUnitOfWork unitOfWork) : base(unitOfWork)
        {           
            When(p => !string.IsNullOrWhiteSpace(p.Name), () =>
            {
                _ = RuleFor(p => p).Custom((p, context) =>
                {
                    if (_Entities.IsCountryExist(p.Name))
                    {
                        context.AddFailure(new ValidationFailure($"Name", $"Country is already exists."));
                    }
                });                
            }).Otherwise(() =>
            {
                _ = RuleFor(p => p.Name).Custom((x, context) =>
                {
                    context.AddFailure(new ValidationFailure($"Name", $"Country Name is Empty.,"));
                });
            });            
        }

    }
}
