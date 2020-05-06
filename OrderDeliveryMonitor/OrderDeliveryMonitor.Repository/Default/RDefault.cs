using OrderDeliveryMonitor.DataAccessLibrary.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Repository.Default
{
    public class RDefault<T> : IRDefault<T>
        where T : class
    {
        private readonly OrderDeliveryMonitorDataContext _context;

        public RDefault()
        {
            this._context = new OrderDeliveryMonitorDataContext();
        }

        public void Create(T pEntity)
        {
            throw new NotImplementedException();
        }

        public void Update(T pEntity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T pEntity)
        {
            throw new NotImplementedException();
        }        

        public T Get(T pEntity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> pWhereClause)
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction()
        {
            this._context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            this._context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            this._context.Database.RollbackTransaction();
        }

        public void Dispose()
        {
        }
    }
}
