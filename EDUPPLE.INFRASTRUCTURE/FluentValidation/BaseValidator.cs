using EDUPPLE.DOMAIN.Entities;
using EDUPPLE.DOMAIN.Interface;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EDUPPLE.INFRASTRUCTURE.FluentValidation
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
        where T : class
    {
        protected readonly IUnitOfWork DataContext;

        protected BaseValidator(IUnitOfWork dataContext)
        {

            DataContext = dataContext;

        }
    }
    public abstract class BaseValidator<T, TEntity> : AbstractValidator<T>
       where TEntity : class
    {
        protected readonly IUnitOfWork DataContext;
        protected DbSet<TEntity> _Entities;

        protected BaseValidator(IUnitOfWork dataContext)
        {

            DataContext = dataContext;
            _Entities = DataContext.Set<TEntity>();

        }
    }
    public abstract class BaseValidator<T, TEntity, TKey> : AbstractValidator<T>
         where TEntity : Entity<TKey>
    {
        protected readonly IUnitOfWork DataContext;
        protected DbSet<TEntity> _Entities;
        protected BaseValidator(IUnitOfWork dataContext)
        {
            DataContext = dataContext;
            _Entities = DataContext.Set<TEntity>();
        }
    }
}
