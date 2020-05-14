using OrderDeliveryMonitor.Repository.Implementation.Operation;
using OrderDeliveryMonitor.Repository.Interface.Operation;

namespace OrderDeliveryMonitor.Business.Validation.Operation
{
    public class BOrderValidation
    {
        protected readonly IROrder _orderRepository;

        public BOrderValidation()
        {
            _orderRepository = new ROrder();
        }
    }
}
