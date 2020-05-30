using OrderDeliveryMonitor.Facade.Implementation.Operation.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Services.Operation.Interface
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAll();
        Task<OrderDTO> Get(string pOrderCode);
        Task PutInLine(string pOrderCode);
    }
}
