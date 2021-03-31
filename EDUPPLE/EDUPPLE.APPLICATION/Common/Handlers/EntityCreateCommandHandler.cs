using AutoMapper;
using EDUPPLE.APPLICATION.Common.Commands;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.DOMAIN.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Common.Handlers
{
    public class EntityCreateCommandHandler<TUnitOfWork, TEntity, TKey, TModel, TReadModel>
       : EntityDataContextHandlerBase<TUnitOfWork, TEntity, TKey, TReadModel, EntityCreateCommand<TModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>
       where TUnitOfWork : IUnitOfWork
       where TEntity : class, IHaveIdentifier<TKey>, new()
       where TReadModel : class
    {      
        public EntityCreateCommandHandler(ILoggerFactory loggerFactory,
            TUnitOfWork dataContext,
            IMapper mapper            )
            :base(loggerFactory,dataContext,mapper)
        {
        }

        protected override async Task<EntityResponseModel<TReadModel>> ProcessAsync(EntityCreateCommand<TModel, EntityResponseModel<TReadModel>> request, CancellationToken cancellationToken)
        {
            var EntityResponse = new EntityResponseModel<TReadModel>();
            try
            {
                var entity = Mapper.Map<TEntity>(request.Model);
                var dbSet = DataContext.Set<TEntity>();               
                await dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
                if (request.IsTrans) await DataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                await DataContext.SaveChangesNoConflicAsync(cancellationToken).ConfigureAwait(false);

                if(request.IsTrans) DataContext.CommitTransaction();

                EntityResponse.StatusCode = StatusCodes.Status200OK;
                EntityResponse.ReturnStatus = true;
                EntityResponse.Data = !string.IsNullOrEmpty(request.ChildEntities) ? await Read(entity.Id, request.ChildEntities, cancellationToken)
                                      : await Read(entity.Id, cancellationToken);
            }
            catch (Exception ex)
            {
                EntityResponse.StatusCode = StatusCodes.Status422UnprocessableEntity;
                EntityResponse.ReturnStatus = false;
                EntityResponse.ReturnMessage.Add(string.Format("Unable to Insert Record {0}" + ex.Message, typeof(TEntity).Name));
                DataContext.RollbackTransaction();                          

            }
            return EntityResponse;

        }
    }
}
