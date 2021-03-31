using AutoMapper;
using AutoMapper.QueryableExtensions;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.APPLICATION.Common.Queries;
using EDUPPLE.DOMAIN.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Common.Handlers
{

    public class EntitySingleQueryHandler<TUnitOfWork, TEntity, TReadModel> :
         DataContextHandlerBase<TUnitOfWork, EntitySingleQuery<TEntity, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>
        where TEntity : class
        where TUnitOfWork : IUnitOfWork
        where TReadModel : class
    {
        private static readonly Lazy<TReadModel> _emptyList = new Lazy<TReadModel>();
        private readonly IConfigurationProvider _configurationProvider;
        public EntitySingleQueryHandler(ILoggerFactory loggerFactory,
            TUnitOfWork dataContext,
            IMapper mapper, IConfigurationProvider configurationProvider) : base(loggerFactory, dataContext, mapper)
        {
            _configurationProvider = configurationProvider;
        }
        protected override async Task<EntityResponseModel<TReadModel>> ProcessAsync(EntitySingleQuery<TEntity, EntityResponseModel<TReadModel>> request, CancellationToken cancellationToken)
        {

            var entityResponse = new EntityResponseModel<TReadModel>();
            try
            {
                var entityQuery = request.Query;
                var query = entityQuery.Query.Filter(DataContext.Set<TEntity>().AsNoTracking());
                if (!string.IsNullOrEmpty(entityQuery.Query.IncludeProperties))
                {
                    foreach (var includeProperty in entityQuery.Query.IncludeProperties.Split
                        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }
                var total = await query
                .CountAsync(cancellationToken)
                .ConfigureAwait(false);

                if (total == 0)
                    return new EntityResponseModel<TReadModel>
                    {
                        StatusCode = StatusCodes.Status422UnprocessableEntity,
                        ReturnStatus = false,
                        Data = _emptyList.Value
                    };

                var model = await query
               .ProjectTo<TReadModel>(_configurationProvider)
               .FirstOrDefaultAsync(cancellationToken)
               .ConfigureAwait(false);

                entityResponse.StatusCode = StatusCodes.Status200OK;
                entityResponse.ReturnStatus = true;
                entityResponse.Data = model;

            }
            catch (Exception ex)
            {
                entityResponse.StatusCode = StatusCodes.Status422UnprocessableEntity;
                entityResponse.ReturnStatus = false;
                entityResponse.Data = _emptyList.Value;
            }

            return entityResponse;


        }
    }
}
