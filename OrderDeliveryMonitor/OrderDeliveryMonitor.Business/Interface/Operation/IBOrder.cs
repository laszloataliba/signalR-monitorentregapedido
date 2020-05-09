using OrderDeliveryMonitor.Business.Default;
using OrderDeliveryMonitor.Model.Operation;

namespace OrderDeliveryMonitor.Business.Interface.Operation
{
    public interface IBOrder : IBDefault<Order>
    {
        void ToAwaiting(Order pOrder);

        void ToPreparing(Order pOrder, EOrderCommand pCommand);

        void ToFinished(Order pOrder, EOrderCommand pCommand);
    }
}
