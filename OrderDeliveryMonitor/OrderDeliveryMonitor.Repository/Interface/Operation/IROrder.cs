using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Repository.Default;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Repository.Interface.Operation
{
    public interface IROrder : IRDefault<Order>
    {
        //void ToAwaiting(Order pOrder);

        //void ToPreparing(Order pOrder);

        //void ToReady(Order pOrder);

        Task ToAwaiting(Order pOrder);

        Task ToPreparing(Order pOrder);

        Task ToReady(Order pOrder);
    }
}
