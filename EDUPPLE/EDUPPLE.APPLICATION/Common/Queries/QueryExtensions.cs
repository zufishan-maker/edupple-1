using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using EDUPPLE.APPLICATION.Common.Helper;

namespace EDUPPLE.APPLICATION.Common.Queries
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, IEnumerable<EntitySort> sorts)
        {
            if (sorts == null || !sorts.Any())
                return query;

            // Create ordering expression e.g. Field1 asc, Field2 desc
            var builder = new StringBuilder();
            foreach (var sort in sorts)
            {
                if (builder.Length > 0)
                    builder.Append(",");

                builder.Append(sort.Name).Append(" ");

                var isDescending = !string.IsNullOrWhiteSpace(sort.Direction)
                    && sort.Direction.StartsWith("desc", StringComparison.OrdinalIgnoreCase);

                builder.Append(isDescending ? "desc" : "asc");
            }

            return query.OrderBy(builder.ToString());
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> query, EntityFilter filter)
        {
            if (filter == null)
                return query;

            var builder = new LinqExpressionBuilder();
            builder.Build(filter);

            var predicate = builder.Expression;
            var parameters = builder.Parameters.ToArray();

            return query.Where(predicate, parameters);
        }

    }
}
