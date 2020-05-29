using OrderDeliveryMonitor.Business.Default;
using OrderDeliveryMonitor.Model.Operation;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Business.Interface.Operation
{
    public interface IBOrder : IBDefault<Order>
    {
        //void ToAwaiting(Order pOrder);

        //void ToPreparing(Order pOrder, EOrderCommand pCommand);

        //void ToReady(Order pOrder, EOrderCommand pCommand);

        Task ToAwaiting(Order pOrder);

        Task ToPreparing(Order pOrder, EOrderCommand pCommand);

        Task ToReady(Order pOrder, EOrderCommand pCommand);
    }
}
