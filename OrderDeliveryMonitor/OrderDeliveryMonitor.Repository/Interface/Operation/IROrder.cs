using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Repository.Default;

namespace OrderDeliveryMonitor.Repository.Interface.Operation
{
    public interface IROrder : IRDefault<Order>
    {
        void ToAwaiting(Order pOrder);

        void ToPreparing(Order pOrder);

        void ToFinished(Order pOrder);
    }
}
