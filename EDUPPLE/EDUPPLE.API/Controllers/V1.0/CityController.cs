using EDUPPLE.API.Helper;
using EDUPPLE.APPLICATION.City.Models;
using EDUPPLE.APPLICATION.Common.Commands;
using EDUPPLE.APPLICATION.Common.Helper;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.APPLICATION.Common.Queries;
using EDUPPLE.INFRASTRUCTURE.Extensions;
using EDUPPLE.INFRASTRUCTURE.Invariant;
using EDUPPLE.INFRASTRUCTURE.Swagger;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.API.Controllers
{
    [ApiVersion(Swagger.Versions.v1_0)]
    [Route(AspNet.Mvc.DefaultControllerTemplate)]
    [Produces(MediaTypeNames.Application.Json)]
    public class CityController : BaseController
    {
        public CityController(IMediator mediator, ICurrentUser currentUser)
         : base(mediator, currentUser)
        {

        }
        [HttpGet]
        [Route("GetCitiesByCountryId")]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseListModel<CityReadModel>), 200)]
        public async Task<IActionResult> GetCitiesByCountryId(int countryId, CancellationToken cancellationToken = default(CancellationToken))
        {         
            var filter = Query<DOMAIN.Entities.City>.Create(x => x.CountryId == countryId);
            filter.IncludeProperties = @"Country";
            var search = new EntityQuery<DOMAIN.Entities.City>(filter, 1, int.MaxValue, string.Empty);
            var command = new EntityListQuery<DOMAIN.Entities.City, EntityResponseListModel<CityReadModel>>(search, CurrentUser);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<CityReadModel>.GenerateResponseList(result);
        }

        [HttpPost]
        [Route(AspNet.Mvc.ActionTemplate)]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseModel<CityReadModel>), 200)]
        public async Task<IActionResult> Insert(CityCreateModel model, CancellationToken cancellationToken)
        {

            var command = new EntityCreateCommand<CityCreateModel, EntityResponseModel<CityReadModel>>(CurrentUser, model,false);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<CityReadModel>.GenerateResponse(result);
        }
        [HttpPut]
        [Route(AspNet.Mvc.ActionTemplate)]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseModel<CityReadModel>), 200)]
        public async Task<IActionResult> Update(int id, CityUpdateModel model, CancellationToken cancellationToken)
        {
            var command = new EntityUpdateCommand<int, CityUpdateModel, EntityResponseModel<CityReadModel>>(CurrentUser, id, model,false);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<CityReadModel>.GenerateResponse(result);
        }
        [HttpDelete]      
        [Route(AspNet.Mvc.ActionTemplate)]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseModel<CityReadModel>), 200)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {

            var command = new EntityDeleteCommand<int, EntityResponseModel<CityReadModel>>(CurrentUser, id, false);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<CityReadModel>.GenerateResponse(result);

        }
        [HttpGet]
        [Route(AspNet.Mvc.ActionTemplate + "/{id}")]
        [MapToApiVersion(Swagger.Versions.v1_0)]
        [ApiExplorerSettings(GroupName = Swagger.DocVersions.v1_0)]
        [ProducesResponseType(typeof(EntityResponseModel<CityReadModel>), 200)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var command = new EntityIdentifierQuery<int, EntityResponseModel<CityReadModel>>(CurrentUser, id);
            var result = await Mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return ResponseHelper<CityReadModel>.GenerateResponse(result);
        }
    }
}
