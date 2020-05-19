using OrderDeliveryMonitor.Facade.Default;
using OrderDeliveryMonitor.Facade.Implementation.Operation.DTO;
using OrderDeliveryMonitor.Model.Operation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OrderDeliveryMonitor.Facade.Interface.Operation
{
    public interface IFOrder : IFDefault<Order>
    {
        void ToAwaiting(Order pOrder);
        void ToPreparing(Order pOrder, EOrderCommand pCommand);
        void ToReady(Order pOrder, EOrderCommand pCommand);
        OrderDTO GetOrderDTO(Expression<Func<Order, bool>> pWhereClause, Expression<Func<Order, object>> pInclude = null);
        IEnumerable<OrderDTO> GetListOrderDTO(Expression<Func<Order, bool>> pWhereClause = null, Expression<Func<Order, object>> pInclude = null);
    }
}
