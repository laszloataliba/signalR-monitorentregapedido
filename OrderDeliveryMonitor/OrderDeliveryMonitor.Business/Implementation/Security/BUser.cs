using OrderDeliveryMonitor.Business.Interface.Security;
using OrderDeliveryMonitor.Business.Validation.Security;
using OrderDeliveryMonitor.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Business.Implementation.Security
{
    public class BUser : BUserValidation, IBUser
    {
        public BUser() :
            base()
        {
        }

        public void Create(User pEntity)
        {
            _userRepository.Create(pEntity);
        }

        public void Delete(User pEntity)
        {
            _userRepository.Delete(pEntity);
        }

        public User Get(Expression<Func<User, bool>> pEntity)
        {
            return _userRepository.Get(pEntity);
        }

        public IEnumerable<User> GetList(Expression<Func<User, bool>> pWhereClause)
        {
            return _userRepository.GetList(pWhereClause);
        }

        public void Update(User pEntity)
        {
            _userRepository.Update(pEntity);
        }
    }
}
