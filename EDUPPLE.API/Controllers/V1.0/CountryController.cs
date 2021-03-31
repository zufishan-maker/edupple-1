
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.APPLICATION.Country.Models;
using EDUPPLE.APPLICATION.Common.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using EDUPPLE.API.Helper;
using EDUPPLE.APPLICATION.Common.Commands;
using EDUPPLE.APPLICATION.Common.Helper;
using EDUPPLE.INFRASTRUCTURE.Extensions;
using EDUPPLE.INFRASTRUCTURE.Swagger;
using EDUPPLE.INFRASTRUCTURE.Invariant;
using System.Net.Mime;

namespace EDUPPLE.API.Controllers
{
    [ApiVersion(Swagger.Versions.v1_0)]
    [Route(AspNet.Mvc.DefaultControllerTemplate)]
    [Produces(MediaTypeNames.Application.Json)]
    public class CountryController : BaseController
    {
        public CountryController(IMediator mediator, ICurrentUser currentUser)
           : base(mediator,currentUser)
        {

        }
        [HttpGet]
        [Route(AspNet.Mvc.ActionTemplate)]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseListModel<CountryReadModel>), 200)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default(CancellationToken))
        {            
            var search = new EntityQuery<DOMAIN.Entities.Country>(null, 1, int.MaxValue, string.Empty);
            var command = new EntityListQuery<DOMAIN.Entities.Country, EntityResponseListModel<CountryReadModel>>(search,CurrentUser);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<CountryReadModel>.GenerateResponseList(result);
        }
        [HttpPost]
        [Route(AspNet.Mvc.ActionTemplate)]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseModel<CountryReadModel>), 200)]
        public async Task<IActionResult> Insert(CountryCreateModel model, CancellationToken cancellationToken)
        {

            var command = new EntityCreateCommand<CountryCreateModel, EntityResponseModel<CountryReadModel>>(CurrentUser, model,false);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<CountryReadModel>.GenerateResponse(result);
        }
        [HttpPut]
        [Route(AspNet.Mvc.ActionTemplate)]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseModel<CountryReadModel>), 200)]
        public async Task<IActionResult> Update(int id, CountryUpdateModel model, CancellationToken cancellationToken)
        {
            var command = new EntityUpdateCommand<int, CountryUpdateModel, EntityResponseModel<CountryReadModel>>(CurrentUser, id, model,false);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<CountryReadModel>.GenerateResponse(result);
        }
        [HttpDelete]
        [Route(AspNet.Mvc.ActionTemplate)]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseModel<CountryReadModel>), 200)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {

            var command = new EntityDeleteCommand<int, EntityResponseModel<CountryReadModel>>(CurrentUser, id,false);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<CountryReadModel>.GenerateResponse(result);

        }

        [HttpGet]
        [Route(AspNet.Mvc.ActionTemplate + "/{id}")]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseModel<CountryReadModel>), 200)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var command = new EntityIdentifierQuery<int, EntityResponseModel<CountryReadModel>>(CurrentUser, id);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<CountryReadModel>.GenerateResponse(result);
        }
    }
}
