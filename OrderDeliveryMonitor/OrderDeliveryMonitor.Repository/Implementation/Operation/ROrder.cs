using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Repository.Default;
using OrderDeliveryMonitor.Repository.Interface.Operation;

namespace OrderDeliveryMonitor.Repository.Implementation.Operation
{
    public class ROrder : RDefault<Order>, IROrder
    {
        public void ToAwaiting(Order pOrder)
        {
            base.Update(pOrder);
        }

        public void ToPreparing(Order pOrder)
        {
            base.Update(pOrder);
        }

        public void ToReady(Order pOrder)
        {
            base.Update(pOrder);
        }
    }
}
