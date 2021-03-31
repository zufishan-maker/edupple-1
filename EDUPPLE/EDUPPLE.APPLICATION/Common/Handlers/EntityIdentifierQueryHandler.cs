using AutoMapper;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.APPLICATION.Common.Queries;
using EDUPPLE.DOMAIN.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Common.Handlers
{
    public class EntityIdentifierQueryHandler<TUnitOfWork, TEntity, TKey, TReadModel>
       : EntityDataContextHandlerBase<TUnitOfWork, TEntity, TKey, TReadModel,
         EntityIdentifierQuery<TKey, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>
       where TEntity : class, IHaveIdentifier<TKey>, new()
       where TUnitOfWork : IUnitOfWork
       where TReadModel : class
    {
      
        public EntityIdentifierQueryHandler(ILoggerFactory loggerFactory,
            TUnitOfWork dataContext,
            IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {
        }
        protected override async Task<EntityResponseModel<TReadModel>> ProcessAsync(EntityIdentifierQuery<TKey, EntityResponseModel<TReadModel>> request, CancellationToken cancellationToken)
        {
            var entityResponse = new EntityResponseModel<TReadModel>();
            try
            {
                var model = await Read(request.Id, request.IncludeProperties, cancellationToken)
                    .ConfigureAwait(false);

                entityResponse.StatusCode = StatusCodes.Status200OK;
                entityResponse.ReturnStatus = true;
                entityResponse.Data = model;
            }
            catch (Exception ex)
            {
                var info = String.Format("Error Record from {0} - with Id {1}" + ex.Message, typeof(TEntity).Name, request.Id.ToString());                
                entityResponse.StatusCode = StatusCodes.Status404NotFound;
                entityResponse.ReturnMessage.Add(info);
                entityResponse.ReturnStatus = false;
                Logger.LogError(info);
            }
            return entityResponse;
        }
    }
}
