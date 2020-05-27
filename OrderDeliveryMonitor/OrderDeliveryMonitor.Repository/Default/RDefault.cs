using Microsoft.EntityFrameworkCore;
using OrderDeliveryMonitor.DataAccessLibrary.Context;
using OrderDeliveryMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Repository.Default
{
    /// <summary>
    /// Default generic repository class.
    /// </summary>
    /// <typeparam name="T">Object name.</typeparam>
    public class RDefault<T> : IRDefault<T>
        where T : class
    {
        /// <summary>
        /// Context object. Provides the access to database inside this class.
        /// </summary>
        private readonly OrderDeliveryMonitorDataContext _context;

        /// <summary>
        /// Context object. Provides access to database for objects that inherits this class.
        /// </summary>
        protected OrderDeliveryMonitorDataContext Context => this._context;

        /// <summary>
        /// RDefault constructor method.
        /// </summary>
        public RDefault()
        {
            this._context = new OrderDeliveryMonitorDataContext();
        }

        /// <summary>
        /// Creates the record in data base.
        /// </summary>
        /// <param name="pEntity"></param>
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

        /// <summary>
        /// Updates the record in data base.
        /// </summary>
        /// <param name="pEntity"></param>
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

        /// <summary>
        /// Deletes the record in data base.
        /// </summary>
        /// <param name="pEntity"></param>
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

        /// <summary>
        /// Gets a specific record in data base, according to the given parameters.
        /// </summary>
        /// <param name="pWhereClause">Condition clause.</param>
        /// <param name="pInclude">Object to be included in the query.</param>
        /// <returns>Object requested.</returns>
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
                else
                    vQuery = this._context.Set<T>().AsQueryable().Where(pWhereClause);

                var vEntity = vQuery.SingleOrDefault<T>();

                return vEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets a list of records from data base, according to the given parameters.
        /// </summary>
        /// <param name="pWhereClause">Condition clause.</param>
        /// <param name="pInclude">Object to be included in the query.</param>
        /// <param name="pPagination">Pagination object.</param>
        /// <returns>List of requested objects.</returns>
        public virtual IEnumerable<T> GetList(
            Expression<Func<T, bool>> pWhereClause = null, 
            Expression<Func<T, object>> pInclude = null, 
            Pagination pPagination = null)
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

                if (pPagination != null)
                {
                    pPagination.TotalRecords = vQuery.Count();
                    pPagination.TotalPages = 
                        (int)Math.Ceiling((double)vQuery.Count() / pPagination.PageSize);

                    vQuery = vQuery.Skip(pPagination.Skip).Take(pPagination.Take);
                }

                var vEntities = vQuery.ToList();

                return vEntities;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method that starts the control of the current transaction in data base.
        /// </summary>
        public void BeginTransaction()
        {
            this._context.Database.BeginTransaction();
        }

        /// <summary>
        /// Method that commits the current transaction controlled in data base.
        /// </summary>
        public void CommitTransaction()
        {
            this._context.Database.CommitTransaction();
        }

        /// <summary>
        /// Method that rollbacks the current transaction controlled in data base.
        /// </summary>
        public void RollbackTransaction()
        {
            this._context.Database.RollbackTransaction();
        }

        public void Dispose()
        {
        }
    }
}
