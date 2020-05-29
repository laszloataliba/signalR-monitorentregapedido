using OrderDeliveryMonitor.Business.Implementation.Operation;
using OrderDeliveryMonitor.Business.Interface.Operation;
using OrderDeliveryMonitor.Facade.Implementation.Operation.DTO;
using OrderDeliveryMonitor.Facade.Interface.Operation;
using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public async Task<Order> GetAsync(Expression<Func<Order, bool>> pWhereClause, Expression<Func<Order, object>> pInclude = null)
        {
            return await _orderBusiness.GetAsync(pWhereClause, pInclude);
        }

        public Order Get(Expression<Func<Order, bool>> pWhereClause, Expression<Func<Order, object>> pInclude = null)
        {
            return this._orderBusiness.Get(pWhereClause, pInclude);
        }

        public IEnumerable<Order> GetList(Expression<Func<Order, bool>> pWhereClause = null, Expression<Func<Order, object>> pInclude = null, Pagination pPagination = null)
        {
            return this._orderBusiness.GetList(pWhereClause, pInclude, pPagination);
        }

        public async Task<IEnumerable<Order>> GetListAsync(Expression<Func<Order, bool>> pWhereClause = null, Expression<Func<Order, object>> pInclude = null, Pagination pPagination = null)
        {
            return await _orderBusiness.GetListAsync(pWhereClause, pInclude, pPagination);
        }

        public async Task UpdateAsync(Order pEntity)
        {
            await _orderBusiness.UpdateAsync(pEntity);
        }

        public void Update(Order pEntity)
        {
            this._orderBusiness.Update(pEntity);
        }

        public async Task ToAwaiting(Order pOrder)
        {
            await this._orderBusiness.ToAwaiting(pOrder);
        }

        public async Task ToReady(Order pOrder, EOrderCommand pCommand)
        {
            await this._orderBusiness.ToReady(pOrder, pCommand);
        }

        public async Task ToPreparing(Order pOrder, EOrderCommand pCommand)
        {
            await this._orderBusiness.ToPreparing(pOrder, pCommand);
        }

        public async Task<OrderDTO> GetOrderDTO(Expression<Func<Order, bool>> pWhereClause, Expression<Func<Order, object>> pInclude = null)
        {
            var oOrder = await this._orderBusiness.GetAsync(pWhereClause, pInclude);

            return
                new OrderDTO
                {
                    OrderId = oOrder.OrderId,
                    OrderNumber = oOrder.OrderNumber,
                    OrderCode = oOrder.OrderCode,
                    SellingStation = oOrder.SellingStation,
                    Cashier = oOrder.Cashier,
                    Process = oOrder.Process,
                    Command = oOrder.Command,
                    AwaitingStart = oOrder.AwaitingStart,
                    AwaitingEnd = oOrder.AwaitingEnd,
                    PreparingStart = oOrder.PreparingStart,
                    PreparingEnd = oOrder.PreparingEnd,
                    ReadyStart = oOrder.ReadyStart,
                    ReadEnd = oOrder.ReadyEnd,
                    RedeemDate = oOrder.RedeemDate,
                    Items = oOrder.Items
                };
        }

        public async Task<IEnumerable<OrderDTO>> GetListOrderDTO(
            Expression<Func<Order, bool>> pWhereClause = null,
            Expression<Func<Order, object>> pInclude = null,
            Pagination pPagination = null)
        {
            List<OrderDTO> oOrderDTOs = new List<OrderDTO>();

            var oOrders = await this._orderBusiness.GetListAsync(pWhereClause, pInclude, pPagination);

            foreach (var order in oOrders)
                oOrderDTOs.Add(
                    new OrderDTO
                    {
                        OrderId = order.OrderId,
                        OrderNumber = order.OrderNumber,
                        OrderCode = order.OrderCode,
                        SellingStation = order.SellingStation,
                        Cashier = order.Cashier,
                        Process = order.Process,
                        Command = order.Command,
                        AwaitingStart = order.AwaitingStart,
                        AwaitingEnd = order.AwaitingEnd,
                        PreparingStart = order.PreparingStart,
                        PreparingEnd = order.PreparingEnd,
                        ReadyStart = order.ReadyStart,
                        ReadEnd = order.ReadyEnd,
                        RedeemDate = order.RedeemDate,
                        Items = order.Items
                    }
                );

            return oOrderDTOs;
        }
    }
}
