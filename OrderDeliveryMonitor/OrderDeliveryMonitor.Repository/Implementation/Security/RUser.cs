using OrderDeliveryMonitor.Model.Security;
using OrderDeliveryMonitor.Repository.Default;
using OrderDeliveryMonitor.Repository.Interface.Security;

namespace OrderDeliveryMonitor.Repository.Implementation.Security
{
    public class RUser : RDefault<User>, IRUser
    {
    }
}
