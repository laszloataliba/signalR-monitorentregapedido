using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Repository.Default;
using OrderDeliveryMonitor.Repository.Interface.Operation;

namespace OrderDeliveryMonitor.Repository.Implementation.Operation
{
    public class ROrder : RDefault<Order>, IROrder
    {
    }
}
