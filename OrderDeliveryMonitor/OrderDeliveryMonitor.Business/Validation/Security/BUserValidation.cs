using OrderDeliveryMonitor.Repository.Implementation.Security;
using OrderDeliveryMonitor.Repository.Interface.Security;

namespace OrderDeliveryMonitor.Business.Validation.Security
{
    public class BUserValidation
    {
        protected readonly IRUser _userRepository;

        public BUserValidation()
        {
            _userRepository = new RUser();
        }
    }
}
