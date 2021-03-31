using EDUPPLE.DOMAIN.Entities;
using EDUPPLE.DOMAIN.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace EDUPPLE.INFRASTRUCTURE.Configurations
{

    public abstract class BaseConfiguration<T> : IBaseEntityConfiguration where T : class
    {
        protected readonly ModelBuilder _modelBuilder;
        public static IEnumerable<System.Security.Claims.ClaimsIdentity> _claims;
        protected EntityTypeBuilder<T> _builder => _modelBuilder.Entity<T>();
        protected BaseConfiguration(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }
    }
    public abstract class BaseConfiguration<T, TKey> : IEntityConfiguration where T : Entity<TKey>
    {
        protected readonly ModelBuilder _modelBuilder;
        public static IEnumerable<System.Security.Claims.ClaimsIdentity> _claims;
        protected EntityTypeBuilder<T> _builder => _modelBuilder.Entity<T>();
        protected BaseConfiguration(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public virtual void Configure()
        {
            _builder.HasKey(t => t.Id);
            _builder.Property(p => p.IsDeleted);

            // Properties
            _builder.Property(t => t.Id)
                .IsRequired();


        }


    }
}
