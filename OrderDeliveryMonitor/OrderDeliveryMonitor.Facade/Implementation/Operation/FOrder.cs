using OrderDeliveryMonitor.Business.Implementation.Operation;
using OrderDeliveryMonitor.Business.Interface.Operation;
using OrderDeliveryMonitor.Facade.Implementation.Operation.DTO;
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

        public Order Get(Expression<Func<Order, bool>> pWhereClause, Expression<Func<Order, object>> pInclude = null)
        {
            return this._orderBusiness.Get(pWhereClause, pInclude);
        }

        public IEnumerable<Order> GetList(Expression<Func<Order, bool>> pWhereClause = null, Expression<Func<Order, object>> pInclude = null)
        {
            return this._orderBusiness.GetList(pWhereClause, pInclude);
        }

        public void Update(Order pEntity)
        {
            this._orderBusiness.Update(pEntity);
        }

        public void ToAwaiting(Order pOrder)
        {
            this._orderBusiness.ToAwaiting(pOrder);
        }

        public void ToReady(Order pOrder, EOrderCommand pCommand)
        {
            this._orderBusiness.ToReady(pOrder, pCommand);
        }

        public void ToPreparing(Order pOrder, EOrderCommand pCommand)
        {
            this._orderBusiness.ToPreparing(pOrder, pCommand);
        }

        public OrderDTO GetOrderDTO(Expression<Func<Order, bool>> pWhereClause, Expression<Func<Order, object>> pInclude = null)
        {
            var oOrder = this._orderBusiness.Get(pWhereClause, pInclude);

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

        public IEnumerable<OrderDTO> GetListOrderDTO(Expression<Func<Order, bool>> pWhereClause = null, Expression<Func<Order, object>> pInclude = null)
        {
            List<OrderDTO> oOrderDTOs = new List<OrderDTO>();

            var oOrders = this._orderBusiness.GetList(pWhereClause, pInclude);

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
