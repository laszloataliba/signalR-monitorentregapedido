using OrderDeliveryMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Business.Default
{
    public interface IBDefault<T>
    {
        void Create(T pEntity);
        T Get(Expression<Func<T, bool>> pWhereClause, Expression<Func<T, object>> pInclude = null);
        void Update(T pEntity);
        void Delete(T pEntity);
        IEnumerable<T> GetList(Expression<Func<T, bool>> pWhereClause = null, Expression<Func<T, object>> pInclude = null, Pagination pPagination = null);
    }
}
