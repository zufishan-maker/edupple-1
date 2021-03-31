using AutoMapper;
using AutoMapper.QueryableExtensions;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.APPLICATION.Common.Queries;
using EDUPPLE.DOMAIN.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Common.Handlers
{
    public class EntityListQueryHandler<TUnitOfWork, TEntity, TReadModel> :
        DataContextHandlerBase<TUnitOfWork, EntityListQuery<TEntity, EntityResponseListModel<TReadModel>>, EntityResponseListModel<TReadModel>>
        where TEntity : class
        where TUnitOfWork : IUnitOfWork
        where TReadModel : class
    {
        private static readonly Lazy<List<TReadModel>> _emptyList = new Lazy<List<TReadModel>>(() => new List<TReadModel>());
        private readonly IConfigurationProvider _configurationProvider;

        public EntityListQueryHandler(ILoggerFactory loggerFactory, TUnitOfWork dataContext, IMapper mapper, IConfigurationProvider configurationProvider)
            : base(loggerFactory, dataContext, mapper)
        {
            _configurationProvider = configurationProvider;
        }

        protected override async Task<EntityResponseListModel<TReadModel>> ProcessAsync(EntityListQuery<TEntity, EntityResponseListModel<TReadModel>> request, CancellationToken cancellationToken)
        {

            var entityResponse = new EntityResponseListModel<TReadModel>();
            try
            {
                var entityQuery = request.Query;
                var query = entityQuery.Query != null ? entityQuery.Query.Filter(DataContext.Set<TEntity>().AsNoTracking())
                    : DataContext.Set<TEntity>().AsNoTracking();

                if (entityQuery.Query != null)
                {
                    if (!string.IsNullOrEmpty(entityQuery.Query.IncludeProperties))
                    {
                        foreach (var includeProperty in entityQuery.Query.IncludeProperties.Split
                            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            query = query.Include(includeProperty);
                        }
                    }
                }

                var total = await query
                    .CountAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (total == 0)
                    return new EntityResponseListModel<TReadModel> { Data = _emptyList.Value, StatusCode = StatusCodes.Status200OK, ReturnStatus = true };

                // page the query and convert to read model
                var model = await query
                    .Sort(entityQuery.Sort)
                    .Page(entityQuery.Page, entityQuery.PageSize)
                    .ProjectTo<TReadModel>(_configurationProvider)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                entityResponse.StatusCode = StatusCodes.Status200OK;
                entityResponse.ReturnStatus = true;
                entityResponse.Data = model;

            }
            catch (Exception ex)
            {
                var info = String.Format("Error Get Record {0}" + ex.Message, typeof(TEntity).Name);
                entityResponse.StatusCode = StatusCodes.Status404NotFound;
                entityResponse.ReturnMessage.Add(info);
                entityResponse.ReturnStatus = false;
            }
            return entityResponse;

        }
    }



}
