using EDUPPLE.APPLICATION.Common.Helper;

namespace EDUPPLE.APPLICATION.Common.Queries
{
    public class SingleQuery<TEntity>
    {
        public SingleQuery(IQuery<TEntity> query)
        {
            Query = query;
        }

        public IQuery<TEntity> Query { get; set; }
        public EntityFilter Filter { get; set; }
    }
}
