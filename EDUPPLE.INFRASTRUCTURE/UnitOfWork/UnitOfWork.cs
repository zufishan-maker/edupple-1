using EDUPPLE.DOMAIN.Interface;
using EDUPPLE.INFRASTRUCTURE.Extensions;
using EDUPPLE.INFRASTRUCTURE.Helper;
using EDUPPLE.INFRASTRUCTURE.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EduppleDbContext DbContext;
        private IDbContextTransaction Transaction;
        private IsolationLevel? IsolationLevel;
        private readonly ICurrentUser HttpContextAccessor;
      

        public UnitOfWork(EduppleDbContext context, ICurrentUser httpContextAccessor)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public void BeginTransaction() => NewTransactionIfNeeded();

        public void CommitTransaction()
        {
            DbContext.SaveChanges();
            if (Transaction == null) return;
            Transaction.Commit();
            Transaction.Dispose();
            Transaction = null;
        }

        public void NewTransactionIfNeeded()
        {
            if (Transaction == null)
            {
                Transaction = IsolationLevel.HasValue ? DbContext.Database.BeginTransaction(IsolationLevel.GetValueOrDefault()) : DbContext.Database.BeginTransaction();
            }
        }
        private async Task OnBeforeSaving()
        {
            var changedEntries = DbContext.ChangeTracker
                .Entries()
                .Where(e =>
                    (e.Entity is ITrackCreated || e.Entity is ISoftDelete || e.Entity is ITrackUpdated) &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

            foreach (var entry in changedEntries)
            {
                await TrackAdded(entry);
                await TrackUpdated(entry);
                await TrackDeleted(entry);
            }            
        }

        private async Task TrackAdded(EntityEntry entry)
        {
            if (entry.Entity is ITrackCreated added)
            {

                if (entry.State == EntityState.Added && added.CreatedOn == default)
                {
                    added.CreatedBy = HttpContextAccessor != null ? HttpContextAccessor.UserName : "System Generated";
                    added.CreatedOn = DateTimeOffset.UtcNow;
                }
            }
        }
        private async Task TrackUpdated(EntityEntry entry)
        {

            if (entry.Entity is ITrackUpdated updated)
            {
                if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    updated.UpdatedBy = HttpContextAccessor != null ? HttpContextAccessor.UserName : "System Generated";
                    updated.UpdatedOn = DateTimeOffset.UtcNow;
                }
            }
        }

        private async Task TrackDeleted(EntityEntry entry)
        {

            if (entry.Entity is ISoftDelete deleted)
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["UpdatedOn"] = DateTimeOffset.UtcNow;
                    deleted.IsDeleted = true;
                }
            }
        }
       
        public void RollbackTransaction()
        {
            if (Transaction == null) return;
            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                BeginTransaction();
                await OnBeforeSaving();
                var result = await DbContext.SaveChangesAsync(cancellationToken);
                return result;

            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbConcurrencyException(
                    "The record you attempted to edit was modified by another " +
                    "user after you loaded it. The edit operation was cancelled and the " +
                    "currect values of the record are displayed. Please try again.", exception);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveChangesNoConflicAsync(CancellationToken cancellationToken)
        {
            try
            {
                await OnBeforeSaving();
                var result = await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return result;

            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new DbConcurrencyException(
                    "The record you attempted to edit was modified by another " +
                    "user after you loaded it. The edit operation was cancelled and the " +
                    "currect values of the record are displayed. Please try again.", exception);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DbSet<Y> Set<Y>() where Y : class
        {
            return DbContext.Set<Y>();
        }

       
        public List<Y> SPExecute<Y>(object[] parameters, string spName) where Y : class
        {
            throw new NotImplementedException();
        }

        public List<Y> ExecuteSP<Y>(List<KeyValuePair<string, string>> param, string spName) where Y : class
        {
            throw new NotImplementedException();
        }

        public string GetSchema(object entry)
        {
            throw new NotImplementedException();
        }
    }
}
