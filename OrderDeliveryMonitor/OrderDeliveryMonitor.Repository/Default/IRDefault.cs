using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Repository.Default
{
    public interface IRDefault<T>: IDisposable
    {
        void Create(T pEntity);
        T Get(T pEntity);
        void Update(T pEntity);
        void Delete(T pEntity);
        IEnumerable<T> GetList(Expression<Func<T, bool>> pWhereClause);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
