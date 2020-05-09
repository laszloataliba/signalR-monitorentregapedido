using OrderDeliveryMonitor.Facade.Default;
using OrderDeliveryMonitor.Model.Operation;

namespace OrderDeliveryMonitor.Facade.Interface.Operation
{
    public interface IFOrder : IFDefault<Order>
    {
        void ToAwaiting(Order pOrder);

        void ToPreparing(Order pOrder, EOrderCommand pCommand);

        void ToFinished(Order pOrder, EOrderCommand pCommand);
    }
}
