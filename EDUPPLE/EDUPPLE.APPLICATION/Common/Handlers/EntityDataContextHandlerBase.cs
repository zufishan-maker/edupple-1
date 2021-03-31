using AutoMapper;
using AutoMapper.QueryableExtensions;
using EDUPPLE.DOMAIN.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Common.Handlers
{
    public abstract class EntityDataContextHandlerBase<TUnitOfWork, TEntity, TKey, TReadModel, TRequest, TResponse>
        : DataContextHandlerBase<TUnitOfWork, TRequest, TResponse>
        where TUnitOfWork : IUnitOfWork
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TRequest : IRequest<TResponse>
        where TReadModel : class

    {         
       
        protected EntityDataContextHandlerBase(ILoggerFactory loggerFactory, TUnitOfWork dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {      
        }  
        private async Task<IEnumerable<TReadModel>> ListQuery(CancellationToken cancellationToken)
        {
            var model = await DataContext
                .Set<TEntity>()
                .AsNoTracking()
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            return model.AsEnumerable();
        }

        protected virtual async Task<TReadModel> Read(TKey key, CancellationToken cancellationToken = default(CancellationToken))
        {
            var model = await DataContext
                .Set<TEntity>()
                .AsNoTracking()
                .Where(p => Equals(p.Id, key))
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
            return Mapper.Map<TReadModel>(model);
        }      
        protected virtual async Task<TReadModel> Read(TKey key, string properties, CancellationToken cancellationToken = default(CancellationToken))
        {
            var model = DataContext
                .Set<TEntity>()
                .Where(p => Equals(p.Id, key));

            if (!string.IsNullOrEmpty(properties))
            {
                model = properties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(model, (current, s) => current.Include(s.Trim(new char[] { ' ', '\n', '\r' })));
            }
            var result = await model.FirstOrDefaultAsync().ConfigureAwait(false);
            return Mapper.Map<TReadModel>(result);
        }


    }
}
