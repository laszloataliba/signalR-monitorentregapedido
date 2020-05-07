﻿using OrderDeliveryMonitor.Business.Implementation.Operation;
using OrderDeliveryMonitor.Business.Interface.Operation;
using OrderDeliveryMonitor.Facade.Interface.Operation;
using OrderDeliveryMonitor.Model.Operation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Facade.Implementation.Operation
{
    public class FOrder : IFOrder
    {
        private readonly IBOrder _orderBusiness;

        public FOrder()
        {
            this._orderBusiness = new BOrder();
        }

        public void Create(Order pEntity)
        {
            this._orderBusiness.Create(pEntity);
        }

        public void Delete(Order pEntity)
        {
            this._orderBusiness.Delete(pEntity);
        }

        public Order Get(Expression<Func<Order, bool>> pEntity = null, Expression<Func<Order, bool>> pInclude = null)
        {
            return this._orderBusiness.Get(pEntity, pInclude);
        }

        public IEnumerable<Order> GetList(Expression<Func<Order, bool>> pWhereClause = null, Expression<Func<Order, bool>> pInclude = null)
        {
            return this._orderBusiness.GetList(pWhereClause, pInclude);
        }

        public void Update(Order pEntity)
        {
            this._orderBusiness.Update(pEntity);
        }
    }
}
