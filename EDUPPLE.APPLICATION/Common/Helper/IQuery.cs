using System.Linq;

namespace EDUPPLE.APPLICATION.Common.Helper
{
    public interface IQuery<T>
    {
        IQueryable<T> Filter(IQueryable<T> items);
        string IncludeProperties { get; set; }
    }
}
