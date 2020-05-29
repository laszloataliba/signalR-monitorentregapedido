using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Repository.Default;
using OrderDeliveryMonitor.Repository.Interface.Operation;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Repository.Implementation.Operation
{
    public class ROrder : RDefault<Order>, IROrder
    {
        //public void ToAwaiting(Order pOrder)
        //{
        //    base.Update(pOrder);
        //}

        //public void ToPreparing(Order pOrder)
        //{
        //    base.Update(pOrder);
        //}

        //public void ToReady(Order pOrder)
        //{
        //    base.Update(pOrder);
        //}

        public async Task ToAwaiting(Order pOrder)
        {
            await base.UpdateAsync(pOrder);
        }

        public async Task ToPreparing(Order pOrder)
        {
            await base.UpdateAsync(pOrder);
        }

        public async Task ToReady(Order pOrder)
        {
            await base.UpdateAsync(pOrder);
        }
    }
}
