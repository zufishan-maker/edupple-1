using AutoMapper;
using AutoMapper.QueryableExtensions;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.APPLICATION.Common.Queries;
using EDUPPLE.DOMAIN.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Common.Handlers
{
    public class EntityIdentifiersQueryHandler<TUnitOfWork, TEntity, TKey, TReadModel>
        : EntityDataContextHandlerBase<TUnitOfWork, TEntity, TKey, TReadModel,
          EntityIdentifiersQuery<TKey, EntityResponseListModel<TReadModel>>, EntityResponseListModel<TReadModel>>
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TUnitOfWork : IUnitOfWork
        where TReadModel : class
    {
        [Obsolete]
        public EntityIdentifiersQueryHandler(ILoggerFactory loggerFactory,
            TUnitOfWork dataContext,
            IMapper mapper       )
            : base(loggerFactory, dataContext, mapper)
        {
        }
        protected override async Task<EntityResponseListModel<TReadModel>> ProcessAsync(EntityIdentifiersQuery<TKey, EntityResponseListModel<TReadModel>> request, CancellationToken cancellationToken)
        {
            var entityResponse = new EntityResponseListModel<TReadModel>();
            try
            {
                var model = await DataContext
                    .Set<TEntity>()
                    .AsNoTracking()
                    .Where(p => request.Ids.Contains(p.Id))
                    .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
                entityResponse.ReturnStatus = true;
                entityResponse.Data = model;
            }
            catch (Exception ex)
            {
                entityResponse.ReturnMessage.Add(String.Format("Unable to Get Record {0} in {1}" + ex.Message, request.Ids.ToString(), typeof(TEntity).Name));
                entityResponse.ReturnStatus = false;
            }
            return entityResponse;

        }
    }
}
