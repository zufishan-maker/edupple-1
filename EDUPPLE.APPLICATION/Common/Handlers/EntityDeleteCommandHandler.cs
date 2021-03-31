using AutoMapper;
using EDUPPLE.APPLICATION.Common.Commands;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.DOMAIN.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Common.Handlers
{
    public class EntityDeleteCommandHandler<TUnitOfWork, TEntity, TKey, TReadModel>
        : EntityDataContextHandlerBase<TUnitOfWork, TEntity, TKey, TReadModel, EntityDeleteCommand<TKey, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TUnitOfWork : IUnitOfWork
        where TReadModel : class
    {        
        public EntityDeleteCommandHandler(ILoggerFactory loggerFactory,
            TUnitOfWork dataContext,
            IMapper mapper)
            : base(loggerFactory,dataContext,mapper)
        {
        }
        protected override async Task<EntityResponseModel<TReadModel>> ProcessAsync(EntityDeleteCommand<TKey, EntityResponseModel<TReadModel>> request, CancellationToken cancellationToken)
        {
            var entityResponse = new EntityResponseModel<TReadModel>();
            try
            {
                var dbSet = DataContext
                .Set<TEntity>();
                var keyValue = new object[] { request.Id };
                var entity = dbSet.Where(p => Equals(p.Id, request.Id));

                if (!string.IsNullOrEmpty(request.IncludeProperties))
                {
                    entity = request.IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(entity, (current, includeProperty) =>
                            current.Include(includeProperty.Trim(new char[] { ' ', '\n', '\r' })));
                }
                var model = entity.FirstOrDefault();

                if (model == null)
                {
                    entityResponse.StatusCode = StatusCodes.Status404NotFound;
                    throw new Exception("Unable to delete Record., Record not found.");
                }

                if (request.IsTrans) await DataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                await DataContext.SaveChangesNoConflicAsync(cancellationToken).ConfigureAwait(false);

                if (request.IsTrans) DataContext.CommitTransaction();
                var result =  await DataContext
                    .SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
                
                entityResponse.StatusCode = StatusCodes.Status200OK;
                entityResponse.ReturnStatus = true;             

                entityResponse.Data = default;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error Delete record '{0}' ...", ex.Message);
                entityResponse.StatusCode = StatusCodes.Status400BadRequest;
                entityResponse.ReturnMessage.Add(string.Format("Unable to delete Record {0}" + ex.Message, typeof(TEntity).Name));
                entityResponse.ReturnStatus = false;              
            }
            return entityResponse;
        }

    }
}
