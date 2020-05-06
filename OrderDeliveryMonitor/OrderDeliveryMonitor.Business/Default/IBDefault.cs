using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Business.Default
{
    public interface IBDefault<T>
    {
        void Create(T pEntity);
        T Get(Expression<Func<T>> pEntity);
        void Update(T pEntity);
        void Delete(T pEntity);
        IEnumerable<T> GetList(Expression<Func<T, bool>> pWhereClause);
    }
}
