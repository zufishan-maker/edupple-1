using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EDUPPLE.DOMAIN.Interface
{   
    public interface IUnitOfWork
    {
        DbSet<Y> Set<Y>() where Y : class;
        Task<int> SaveChangesNoConflicAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void NewTransactionIfNeeded();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        string GetSchema(object entry);
        List<Y> ExecuteSP<Y>(List<KeyValuePair<string, string>> param, string spName) where Y : class;
        List<Y> SPExecute<Y>(object[] parameters, string spName) where Y : class;
    }
}
