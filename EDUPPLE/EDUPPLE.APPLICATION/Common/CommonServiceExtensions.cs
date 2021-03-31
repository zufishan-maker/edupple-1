using EDUPPLE.APPLICATION.Common.Behaviours;
using EDUPPLE.APPLICATION.Common.Commands;
using EDUPPLE.APPLICATION.Common.Handlers;
using EDUPPLE.APPLICATION.Common.Models;
using EDUPPLE.APPLICATION.Common.Queries;
using EDUPPLE.DOMAIN.Interface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EDUPPLE.APPLICATION.Common
{
    public static class CommonServiceExtensions
    {
        public static IServiceCollection AddEntityCommands<TUnitOfWork, TEntity, TKey, TReadModel, TCreateModel, TUpdateModel>(this IServiceCollection services)
         where TUnitOfWork : IUnitOfWork
         where TEntity : class, IHaveIdentifier<TKey>, new()
         where TCreateModel : class
         where TUpdateModel : class
         where TReadModel : class
        {
            services
               .AddEntityCreateCommand<TUnitOfWork, TEntity, TKey, TReadModel, TCreateModel>()
               .AddEntityUpdateCommand<TUnitOfWork, TEntity, TKey, TReadModel, TUpdateModel>()
               .AddEntityDeleteCommand<TUnitOfWork, TEntity, TKey, TReadModel>()              
               //queries
               .AddEntityQueries<TUnitOfWork, TEntity, TKey, TReadModel>();
               
            return services;
        }

        public static IServiceCollection AddEntityCreateCommand<TUnitOfWork, TEntity, TKey, TReadModel, TModel>(this IServiceCollection services)
         where TUnitOfWork : IUnitOfWork
         where TEntity : class, IHaveIdentifier<TKey>, new()
         where TModel : class
         where TReadModel : class

        {          
            services.TryAddTransient<IRequestHandler<EntityCreateCommand<TModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityCreateCommandHandler<TUnitOfWork, TEntity, TKey, TModel, TReadModel>>();
            services.AddTransient<IPipelineBehavior<EntityCreateCommand<TModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, ValidateEntityModelCommandBehavior<TModel, TReadModel>>();

            return services;
        }

        public static IServiceCollection AddEntityUpdateCommand<TUnitOfWork, TEntity, TKey, TReadModel, TModel>(this IServiceCollection services)
        where TUnitOfWork : IUnitOfWork
        where TEntity : class, IHaveIdentifier<TKey>, new()
        where TModel : class
        where TReadModel : class
        {
            //update command           
            services.TryAddTransient<IRequestHandler<EntityUpdateCommand<TKey, TModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityUpdateCommandHandler<TUnitOfWork, TEntity, TKey, TModel, TReadModel>>();
            services.TryAddTransient<IPipelineBehavior<EntityUpdateCommand<TKey, TModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, ValidateEntityModelCommandBehavior<TModel, TReadModel>>();
            return services;
        }

        public static IServiceCollection AddEntityDeleteCommand<TUnitOfWork, TEntity, TKey, TReadModel>(this IServiceCollection services)
          where TUnitOfWork : IUnitOfWork
          where TEntity : class, IHaveIdentifier<TKey>, new()
          where TReadModel : class
        {
            services.TryAddTransient<IRequestHandler<EntityDeleteCommand<TKey, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityDeleteCommandHandler<TUnitOfWork, TEntity, TKey, TReadModel>>();
            return services;
        }
        public static IServiceCollection AddEntityQueries<TUnitOfWork, TEntity, TKey, TReadModel>(this IServiceCollection services)
          where TUnitOfWork : IUnitOfWork
          where TEntity : class, IHaveIdentifier<TKey>, new()
          where TReadModel : class
        {
            services.TryAddScoped<IRequestHandler<EntityIdentifierQuery<TKey, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityIdentifierQueryHandler<TUnitOfWork, TEntity, TKey, TReadModel>>();
            services.TryAddScoped<IRequestHandler<EntityIdentifiersQuery<TKey, EntityResponseListModel<TReadModel>>, EntityResponseListModel<TReadModel>>, EntityIdentifiersQueryHandler<TUnitOfWork, TEntity, TKey, TReadModel>>();

            services.TryAddTransient<IRequestHandler<EntityListQuery<TEntity, EntityResponseListModel<TReadModel>>, EntityResponseListModel<TReadModel>>, EntityListQueryHandler<TUnitOfWork, TEntity, TReadModel>>();
            services.TryAddTransient<IRequestHandler<EntitySingleQuery<TEntity, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntitySingleQueryHandler<TUnitOfWork, TEntity, TReadModel>>();

            return services;
        }
    }
}
