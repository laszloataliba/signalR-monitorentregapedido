using OrderDeliveryMonitor.Business.Implementation.Security;
using OrderDeliveryMonitor.Business.Interface.Security;
using OrderDeliveryMonitor.Facade.Interface.Security;
using OrderDeliveryMonitor.Model.Security;
using OrderDeliveryMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Facade.Implementation.Security
{
    public class FUser : IFUser
    {
        private readonly IBUser _userBusiness;

        public FUser()
        {
            _userBusiness = new BUser();
        }

        public void Create(User pEntity)
        {
            this._userBusiness.Create(pEntity);
        }

        public void Delete(User pEntity)
        {
            this._userBusiness.Delete(pEntity);
        }

        public User Get(Expression<Func<User, bool>> pWhereClause, Expression<Func<User, object>> pInclude = null)
        {
            return this._userBusiness.Get(pWhereClause, pInclude);
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> pWhereClause, Expression<Func<User, object>> pInclude = null)
        {
            return await _userBusiness.GetAsync(pWhereClause, pInclude);
        }

        public IEnumerable<User> GetList(Expression<Func<User, bool>> pWhereClause = null, Expression<Func<User, object>> pInclude = null, Pagination pPagination = null)
        {
            return this._userBusiness.GetList(pWhereClause, pInclude, pPagination);
        }

        public async Task<IEnumerable<User>> GetListAsync(Expression<Func<User, bool>> pWhereClause = null, Expression<Func<User, object>> pInclude = null, Pagination pPagination = null)
        {
            return await _userBusiness.GetListAsync(pWhereClause, pInclude, pPagination);
        }

        public void Update(User pEntity)
        {
            this._userBusiness.Update(pEntity);
        }

        public async Task UpdateAsync(User pEntity)
        {
            await _userBusiness.UpdateAsync(pEntity);
        }
    }
}
