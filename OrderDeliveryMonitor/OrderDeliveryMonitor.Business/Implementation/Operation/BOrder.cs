using OrderDeliveryMonitor.Business.Interface.Operation;
using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Repository.Implementation.Operation;
using OrderDeliveryMonitor.Repository.Interface.Operation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Business.Implementation.Operation
{
    public class BOrder : IBOrder
    {
        private readonly IROrder _orderRepositor;

        public BOrder()
        {
            _orderRepositor = new ROrder();
        }

        public void Create(Order pEntity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order pEntity)
        {
            throw new NotImplementedException();
        }

        public Order Get(Expression<Func<Order>> pEntity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetList(Expression<Func<Order, bool>> pWhereClause)
        {
            throw new NotImplementedException();
        }

        public void Update(Order pEntity)
        {
            throw new NotImplementedException();
        }
    }
}
