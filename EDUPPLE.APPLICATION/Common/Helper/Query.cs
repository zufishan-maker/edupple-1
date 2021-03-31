using System;
using System.Linq;
using System.Linq.Expressions;

namespace EDUPPLE.APPLICATION.Common.Helper
{
    public class Query<T> : IQuery<T>
    {
        public string IncludeProperties { get; set; }
        private readonly Expression<Func<T, bool>> _filteredExpression;
        private Query(Expression<Func<T, bool>> filteredExpression) => _filteredExpression = filteredExpression;
        public static Query<T> Create(Expression<Func<T, bool>> expression) => new Query<T>(expression);
        public static Query<T> Create(string propertyName, OperationType operation, object value) => new Query<T>(ExpressionBuilder.Build<T>(propertyName, operation, value));
        public IQueryable<T> Filter(IQueryable<T> items) => items.Where(_filteredExpression);
        public Query<T> And(Query<T> other) => new Query<T>(_filteredExpression.And(other._filteredExpression));
        public Query<T> Or(Query<T> other) => new Query<T>(_filteredExpression.Or(other._filteredExpression));
    }
}
