using OrderDeliveryMonitor.Business.Interface.Security;
using OrderDeliveryMonitor.Business.Validation.Security;
using OrderDeliveryMonitor.Model.Security;
using OrderDeliveryMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public User Get(Expression<Func<User, bool>> pWhereClause, Expression<Func<User, object>> pInclude = null)
        {
            return _userRepository.Get(pWhereClause, pInclude);
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> pWhereClause, Expression<Func<User, object>> pInclude = null)
        {
            return await _userRepository.GetAsync(pWhereClause, pInclude);
        }

        public IEnumerable<User> GetList(Expression<Func<User, bool>> pWhereClause = null, Expression<Func<User, object>> pInclude = null, Pagination pPagination = null)
        {
            return _userRepository.GetList(pWhereClause, pInclude, pPagination);
        }

        public async Task<IEnumerable<User>> GetListAsync(Expression<Func<User, bool>> pWhereClause = null, Expression<Func<User, object>> pInclude = null, Pagination pPagination = null)
        {
            return await _userRepository.GetListAsync(pWhereClause, pInclude, pPagination);
        }

        public void Update(User pEntity)
        {
            _userRepository.Update(pEntity);
        }

        public async Task UpdateAsync(User pEntity)
        {
            await _userRepository.UpdateAsync(pEntity);
        }
    }
}
