using Microsoft.EntityFrameworkCore;
using OrderDeliveryMonitor.DataAccessLibrary.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Repository.Default
{
    public class RDefault<T> : IRDefault<T>
        where T : class
    {
        private readonly OrderDeliveryMonitorDataContext _context;

        protected OrderDeliveryMonitorDataContext Context => this._context;

        public RDefault()
        {
            this._context = new OrderDeliveryMonitorDataContext();
        }

        public virtual void Create(T pEntity)
        {
            try
            {
                this._context.Set<T>().Add(pEntity);

                this._context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Update(T pEntity)
        {
            try
            {
                this._context.Set<T>().Update(pEntity);

                this._context.SaveChanges();

                //TESTAR PARA ATUALIZAR CAMPOS ESPECÍFICOS.
                //this._context.Set<T>();
                //this._context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Delete(T pEntity)
        {
            try
            {
                this._context.Set<T>().Remove(pEntity);

                this._context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public virtual T Get(Expression<Func<T, bool>> pWhereClause, Expression<Func<T, object>> pInclude = null)
        {
            try
            {
                IQueryable<T> vQuery = null;

                if (pInclude != null)
                    vQuery = this._context.Set<T>()
                        .AsQueryable()
                            .Where(pWhereClause)
                                .Include(pInclude);

                var vEntity = vQuery.SingleOrDefault<T>();

                return vEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IEnumerable<T> GetList(Expression<Func<T, bool>> pWhereClause = null, Expression<Func<T, object>> pInclude = null)
        {
            try
            {
                IQueryable<T> vQuery = null;

                if (pWhereClause != null && pInclude != null)
                    vQuery = this._context.Set<T>().AsQueryable().Where(pWhereClause).Include(pInclude);

                else if (pWhereClause != null)
                    vQuery = this._context.Set<T>().AsQueryable().Where(pWhereClause);

                else if (pInclude != null)
                    vQuery = this._context.Set<T>().AsQueryable().Include(pInclude);

                else
                    vQuery = this._context.Set<T>().AsQueryable();

                var vEntities = vQuery.ToList();

                return vEntities;
            }
            catch(Exception ex)
            {
                throw ex;
            }
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
