using EDUPPLE.DOMAIN.Entities;
using EDUPPLE.DOMAIN.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.NameTranslation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EDUPPLE.INFRASTRUCTURE.Persistence
{
    public class EduppleDbContext : IdentityDbContext<User, Role,string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public EduppleDbContext(DbContextOptions<EduppleDbContext> dbOptions)
          : base(dbOptions)
        {
            Database.SetCommandTimeout(TimeSpan.FromMinutes(60));
            this.ChangeTracker.LazyLoadingEnabled = false;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggingFactory.LoggerFactory)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        private void LoadConfiguration(ModelBuilder modelBuilder)
        {
            GetType().Assembly.GetTypes()
              .Where(t => {
                  return ((!t.GetTypeInfo().IsAbstract && typeof(IEntityConfiguration).IsAssignableFrom(t)) || (!t.GetTypeInfo().IsAbstract && typeof(IBaseEntityConfiguration).IsAssignableFrom(t)));
                  })
              .ToList()
              .ForEach(t =>
              {
                  if (typeof(IEntityConfiguration).IsAssignableFrom(t)) ((IEntityConfiguration)Activator.CreateInstance(t, new[] { modelBuilder })).Configure();
                  if (typeof(IBaseEntityConfiguration).IsAssignableFrom(t)) Activator.CreateInstance(t, new[] { modelBuilder });              
              });
        }

        public static void ApplySnakeCaseNames(ModelBuilder modelBuilder)
        {
            var mapper = new NpgsqlSnakeCaseNameTranslator();
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(mapper.TranslateMemberName(entity.GetTableName())); 
                
                foreach (var property in entity.GetProperties())
                {

                    property.SetColumnName(mapper.TranslateMemberName(property.GetColumnBaseName()));
                }
                foreach (var key in entity.GetKeys())
                {
                    key.SetName(mapper.TranslateMemberName(key.GetName()));
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(mapper.TranslateMemberName(key.GetConstraintName()));
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(mapper.TranslateMemberName(index.GetDatabaseName()));
                }
            }
        }
        protected static void ConfigureSoftDeleteFilter<T>(ModelBuilder modelBuilder)
        where T : class, ISoftDelete
        {
            modelBuilder.Entity<T>()
                .HasQueryFilter(e => !e.IsDeleted);
        }
        protected void DeleteFilter(ModelBuilder modelBuilder)
        {
            var deletableEntityTypes = GetType().GetTypeInfo().DeclaredMethods.Single(m => m.Name == nameof(ConfigureSoftDeleteFilter));
            var args = new object[] { modelBuilder };
            var deleteEntityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(t => typeof(ISoftDelete).IsAssignableFrom(t.ClrType));
            foreach (var entityType in deleteEntityTypes)
                deletableEntityTypes.MakeGenericMethod(entityType.ClrType).Invoke(this, args);
        }
        private static void RestrictCascadeDelete(IEnumerable<IMutableEntityType> entityTypes)
        {
            var cascadeFKs = entityTypes.SelectMany(t => t.GetForeignKeys())
                                        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entityTypes = modelBuilder.Model.GetEntityTypes().ToList();
            LoadConfiguration(modelBuilder);
            ApplySnakeCaseNames(modelBuilder);
            RestrictCascadeDelete(entityTypes);
            DeleteFilter(modelBuilder);
        }
    }
}
