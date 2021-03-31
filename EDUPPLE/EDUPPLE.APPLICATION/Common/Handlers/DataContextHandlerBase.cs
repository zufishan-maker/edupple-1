using AutoMapper;
using EDUPPLE.DOMAIN.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EDUPPLE.APPLICATION.Common.Handlers
{
    public abstract class DataContextHandlerBase<TUnitOfWork, TRequest, TResponse>
        : RequestHandlerBase<TRequest, TResponse>
        where TUnitOfWork : IUnitOfWork
        where TRequest : IRequest<TResponse>
    {
        protected DataContextHandlerBase(ILoggerFactory loggerFactory, TUnitOfWork dataContext, IMapper mapper)
            : base(loggerFactory)
        {           
            DataContext = dataContext;
            Mapper = mapper;
        }
        protected DataContextHandlerBase(ILoggerFactory loggerFactory, TUnitOfWork dataContext, IMapper mapper, IHttpContextAccessor context)
            : base(loggerFactory)
        {
           
            DataContext = dataContext;
            Mapper = mapper;
            Context = context;
        }
        protected IHttpContextAccessor Context { get; set; }
        protected TUnitOfWork DataContext { get; }
        protected IMapper Mapper { get; }
    }
}
