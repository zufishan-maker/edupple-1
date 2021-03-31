using EDUPPLE.APPLICATION.Common.Commands;
using EDUPPLE.APPLICATION.Common.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Common.Behaviours
{
    public class ValidateEntityModelCommandBehavior<TEntityModel, TResponse> :
         PipelineBehaviorBase<EntityModelCommand<TEntityModel, EntityResponseModel<TResponse>>, EntityResponseModel<TResponse>>

    {
        private readonly IEnumerable<IValidator> _validator;
        public ValidateEntityModelCommandBehavior(ILoggerFactory loggerFactory, IEnumerable<IValidator<TEntityModel>> validator) : base(loggerFactory)
        {
            _validator = validator;
        }
        protected override async Task<EntityResponseModel<TResponse>> Process(EntityModelCommand<TEntityModel, EntityResponseModel<TResponse>> request, CancellationToken cancellationToken, RequestHandlerDelegate<EntityResponseModel<TResponse>> next)
        {
            var context = new ValidationContext<TEntityModel>(request.Model);
            var failures = _validator
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            return failures.Any()
                ? await Errors(failures).ConfigureAwait(false)
                : await next().ConfigureAwait(false);

        }
        private static async Task<EntityResponseModel<TResponse>> Errors(IEnumerable<ValidationFailure> failures)
        {
            var response = new EntityResponseModel<TResponse>();
            foreach (var failure in failures)
            {
                response.Errors.Add(failure.PropertyName, failure.ErrorMessage);
            }
            response.ReturnStatus = false;
            response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            response.ReturnMessage.Add("Validation Error");
            return await Task.FromResult(response);
        }


    }
}
