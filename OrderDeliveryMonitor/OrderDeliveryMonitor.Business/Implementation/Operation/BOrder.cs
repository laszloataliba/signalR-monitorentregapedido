using OrderDeliveryMonitor.Business.Interface.Operation;
using OrderDeliveryMonitor.Business.Validation.Operation;
using OrderDeliveryMonitor.Model.Operation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Business.Implementation.Operation
{
    public class BOrder : BOrderValidation, IBOrder
    {
        public BOrder() :
            base()
        {
        }

        public void Create(Order pEntity)
        {
            this._orderRepository.Create(pEntity);
        }

        public void Delete(Order pEntity)
        {
            this._orderRepository.Delete(pEntity);
        }

        public Order Get(Expression<Func<Order, bool>> pEntity)
        {
            return this._orderRepository.Get(pEntity);
        }

        public IEnumerable<Order> GetList(Expression<Func<Order, bool>> pWhereClause)
        {
            return this._orderRepository.GetList(pWhereClause);
        }

        public void Update(Order pEntity)
        {
            this._orderRepository.Update(pEntity);
        }
    }
}
