using OrderDeliveryMonitor.Facade.Default;
using OrderDeliveryMonitor.Facade.Implementation.Operation.DTO;
using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Utility;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Facade.Interface.Operation
{
    public interface IFOrder : IFDefault<Order>
    {
        Task ToAwaiting(Order pOrder);
        Task ToPreparing(Order pOrder, EOrderCommand pCommand);
        Task ToReady(Order pOrder, EOrderCommand pCommand);
        Task<OrderDTO> GetOrderDTO(Expression<Func<Order, bool>> pWhereClause, Expression<Func<Order, object>> pInclude = null);
        Task<IEnumerable<OrderDTO>> GetListOrderDTO(Expression<Func<Order, bool>> pWhereClause = null, Expression<Func<Order, object>> pInclude = null, Pagination pPagination = null);
    }
}
