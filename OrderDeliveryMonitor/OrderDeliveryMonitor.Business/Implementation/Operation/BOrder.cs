using OrderDeliveryMonitor.Business.Interface.Operation;
using OrderDeliveryMonitor.Business.Validation.Operation;
using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public async Task<Order> GetAsync(Expression<Func<Order, bool>> pWhereClause, Expression<Func<Order, object>> pInclude = null)
        {
            return GetOrder(await _orderRepository.GetAsync(pWhereClause, pInclude));
        }

        public IEnumerable<Order> GetList(Expression<Func<Order, bool>> pWhereClause = null, Expression<Func<Order, object>> pInclude = null, Pagination pPagination = null)
        {
            return this._orderRepository.GetList(pWhereClause, pInclude, pPagination);
        }

        public async Task<IEnumerable<Order>> GetListAsync(Expression<Func<Order, bool>> pWhereClause = null, Expression<Func<Order, object>> pInclude = null, Pagination pPagination = null)
        {
            return await _orderRepository.GetListAsync(pWhereClause, pInclude, pPagination);
        }

        public void Update(Order pEntity)
        {
            this._orderRepository.Update(pEntity);
        }

        public async Task UpdateAsync(Order pEntity)
        {
            await _orderRepository.UpdateAsync(pEntity);
        }

        public async Task ToAwaiting(Order pOrder)
        {
            pOrder = GetOrder(pOrder.OrderCode);

            pOrder.Process = EOrderProcess.Awaiting;
            pOrder.Command = EOrderCommand.Received;
            pOrder.AwaitingStart = DateTime.Now;

            await _orderRepository.ToAwaiting(pOrder);
        }

        public async Task ToPreparing(Order pOrder, EOrderCommand pCommand)
        {
            pOrder = GetOrder(pOrder.OrderId);

            pOrder.Process = EOrderProcess.Preparing;
            pOrder.Command = pCommand;
            pOrder.AwaitingEnd = DateTime.Now;
            pOrder.PreparingStart = DateTime.Now;

            await _orderRepository.ToPreparing(pOrder);
        }

        public async Task ToReady(Order pOrder, EOrderCommand pCommand)
        {
            pOrder = GetOrder(pOrder.OrderId);

            pOrder.Process = EOrderProcess.Ready;
            pOrder.Command = pCommand;
            pOrder.PreparingEnd = DateTime.Now;
            pOrder.ReadyStart = DateTime.Now;

            await _orderRepository.ToReady(pOrder);
        }
    }
}
