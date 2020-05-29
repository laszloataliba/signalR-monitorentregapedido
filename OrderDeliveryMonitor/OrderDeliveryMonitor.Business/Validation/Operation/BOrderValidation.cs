using Microsoft.AspNetCore.Http;
using OrderDeliveryMonitor.Model.Operation;
using OrderDeliveryMonitor.Repository.Implementation.Operation;
using OrderDeliveryMonitor.Repository.Interface.Operation;
using OrderDeliveryMonitor.Resources;
using OrderDeliveryMonitor.Utility;
using System;

namespace OrderDeliveryMonitor.Business.Validation.Operation
{
    public class BOrderValidation
    {
        protected readonly IROrder _orderRepository;

        public BOrderValidation()
        {
            _orderRepository = new ROrder();
        }

        /// <summary>
        /// Validates the given code and returns order data according to its code.
        /// </summary>
        /// <param name="pOrderCode">Order code.</param>
        /// <returns>Order data.</returns>
        protected Order GetOrder(string pOrderCode)
        {
            if (String.IsNullOrEmpty(pOrderCode))
                throw new CustomException(
                        StatusCodes.Status400BadRequest,
                        ResourceHelper.GetResourceValue(Resource.MSG_ORDER_INCORRECT_ID, pOrderCode)
                    );

            var vOrder = _orderRepository.Get(order => order.OrderCode == pOrderCode);

            return GetOrder(vOrder);
        }

        /// <summary>
        /// Validates the given identifier and returns order data according to its key.
        /// </summary>
        /// <param name="pOrderId">Order identifier(DbIdentity).</param>
        /// <returns>Order data.</returns>
        protected Order GetOrder(int pOrderId)
        {
            if (pOrderId <= 0)
                throw new CustomException(
                        StatusCodes.Status400BadRequest,
                        ResourceHelper.GetResourceValue(Resource.MSG_ORDER_INCORRECT_ID, pOrderId)
                    );

            var vOrder = _orderRepository.Get(order => order.OrderId == pOrderId);

            return GetOrder(vOrder);
        }

        /// <summary>
        /// Validates and returns order data.
        /// </summary>
        /// <param name="pOrder">Order data.</param>
        /// <returns>Order data.</returns>
        protected Order GetOrder(Order pOrder)
        {
            if (pOrder == null)
                throw new CustomException(
                        StatusCodes.Status404NotFound,
                        Resource.MSG_ORDER_NOT_FOUND
                    );

            return pOrder;
        }
    }
}
