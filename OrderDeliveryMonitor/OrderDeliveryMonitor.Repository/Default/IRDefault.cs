using OrderDeliveryMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Repository.Default
{
    public interface IRDefault<T>: IDisposable
    {
        void Create(T pEntity);
        T Get(Expression<Func<T, bool>> pWhereClause, Expression<Func<T, object>> pInclude = null);
        void Update(T pEntity);
        void Delete(T pEntity);
        IEnumerable<T> GetList(Expression<Func<T, bool>> pWhereClause = null, Expression<Func<T, object>> pInclude = null, Pagination pPagination = null);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> pWhereClause = null, Expression<Func<T, object>> pInclude = null, Pagination pPagination = null);
        Task<T> GetAsync(Expression<Func<T, bool>> pWhereClause, Expression<Func<T, object>> pInclude = null);
        Task UpdateAsync(T pEntity);
    }
}
