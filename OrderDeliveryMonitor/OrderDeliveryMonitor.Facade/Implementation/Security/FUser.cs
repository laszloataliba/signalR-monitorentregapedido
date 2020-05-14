using OrderDeliveryMonitor.Business.Implementation.Security;
using OrderDeliveryMonitor.Business.Interface.Security;
using OrderDeliveryMonitor.Facade.Interface.Security;
using OrderDeliveryMonitor.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        public IEnumerable<User> GetList(Expression<Func<User, bool>> pWhereClause = null, Expression<Func<User, object>> pInclude = null)
        {
            return this._userBusiness.GetList(pWhereClause, pInclude);
        }

        public void Update(User pEntity)
        {
            this._userBusiness.Update(pEntity);
        }
    }
}
