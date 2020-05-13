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

        public Order Get(Expression<Func<Order, bool>> pWhereClause, Expression<Func<Order, object>> pInclude = null)
        {
            return this._orderRepository.Get(pWhereClause, pInclude);
        }

        public IEnumerable<Order> GetList(Expression<Func<Order, bool>> pWhereClause = null, Expression<Func<Order, object>> pInclude = null)
        {
            return this._orderRepository.GetList(pWhereClause, pInclude);
        }

        public void Update(Order pEntity)
        {
            this._orderRepository.Update(pEntity);
        }

        public void ToAwaiting(Order pOrder)
        {
            pOrder = Get(order => order.OrderId == pOrder.OrderId);

            pOrder.Process = EOrderProcess.Awaiting;
            pOrder.Command = EOrderCommand.Received;
            pOrder.AwaitingStart = DateTime.Now;

            _orderRepository.ToAwaiting(pOrder);
        }

        public void ToPreparing(Order pOrder, EOrderCommand pCommand)
        {
            pOrder = _orderRepository.Get(order => order.OrderId == pOrder.OrderId);

            pOrder.Process = EOrderProcess.Preparing;
            pOrder.Command = pCommand;
            pOrder.AwaitingEnd = DateTime.Now;
            pOrder.PreparingStart = DateTime.Now;

            _orderRepository.ToPreparing(pOrder);
        }

        public void ToFinished(Order pOrder, EOrderCommand pCommand)
        {
            pOrder = _orderRepository.Get(order => order.OrderId == pOrder.OrderId);

            pOrder.Process = EOrderProcess.Ready;
            pOrder.Command = pCommand;
            pOrder.PreparingEnd = DateTime.Now;
            pOrder.Finished = DateTime.Now;

            _orderRepository.ToFinished(pOrder);
        }
    }
}
