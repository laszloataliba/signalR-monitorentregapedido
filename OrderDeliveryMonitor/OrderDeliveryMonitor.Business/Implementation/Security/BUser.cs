using OrderDeliveryMonitor.Business.Interface.Security;
using OrderDeliveryMonitor.Model.Security;
using OrderDeliveryMonitor.Repository.Implementation.Security;
using OrderDeliveryMonitor.Repository.Interface.Security;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Business.Implementation.Security
{
    public class BUser : IBUser
    {
        private readonly IRUser _userRepository;

        public BUser()
        {
            _userRepository = new RUser();
        }

        public void Create(User pEntity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User pEntity)
        {
            throw new NotImplementedException();
        }

        public User Get(Expression<Func<User>> pEntity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetList(Expression<Func<User, bool>> pWhereClause)
        {
            throw new NotImplementedException();
        }

        public void Update(User pEntity)
        {
            throw new NotImplementedException();
        }
    }
}
