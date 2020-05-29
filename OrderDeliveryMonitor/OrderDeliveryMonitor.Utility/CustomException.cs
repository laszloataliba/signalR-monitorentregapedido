using Microsoft.AspNetCore.Http;
using System;

namespace OrderDeliveryMonitor.Utility
{
    public class CustomException : Exception
    {
        public CustomException(string pMessage = "") : 
            base(pMessage)
        {
        }

        public CustomException(int pStatusCode = StatusCodes.Status500InternalServerError, string pMessage = "") : 
            this(pMessage)
        {
            StatusCode = pStatusCode;
        }

        public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;
    }
}
