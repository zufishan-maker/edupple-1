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
    public class EntityUpdateCommandHandler<TUnitOfWork, TEntity, TKey, TModel, TReadModel>
         : EntityDataContextHandlerBase<TUnitOfWork, TEntity, TKey, TReadModel,
           EntityUpdateCommand<TKey, TModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>
         where TUnitOfWork : IUnitOfWork
         where TEntity : class, IHaveIdentifier<TKey>, new()
         where TReadModel : class
    {
        public EntityUpdateCommandHandler(ILoggerFactory loggerFactory,
            TUnitOfWork dataContext,
            IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {

        }
        protected override async Task<EntityResponseModel<TReadModel>> ProcessAsync(EntityUpdateCommand<TKey, TModel, EntityResponseModel<TReadModel>> request, CancellationToken cancellationToken)
        {
            var entityResponse = new EntityResponseModel<TReadModel>();
            try
            {
                var dbSet = DataContext.Set<TEntity>();
                var keyValue = new object[] { request.Id };
                var entity = await dbSet.FindAsync(keyValue, cancellationToken).ConfigureAwait(false);

                if (entity == null) return default;
                Mapper.Map(request.Model, entity);

                if (request.IsTrans) await DataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                await DataContext.SaveChangesNoConflicAsync(cancellationToken).ConfigureAwait(false);

                if (request.IsTrans) DataContext.CommitTransaction();
               
                entityResponse.StatusCode = StatusCodes.Status200OK;
                entityResponse.ReturnStatus = true;
                entityResponse.Data = await Read(entity.Id, request.ChildEntities, cancellationToken);               
            }
            catch (Exception ex)
            {
                entityResponse.ReturnMessage.Add(String.Format("Unable to Update Record {0}" + ex.Message, typeof(TEntity).Name));
                entityResponse.ReturnStatus = false;
            }
            return entityResponse;
        }
    }
}
