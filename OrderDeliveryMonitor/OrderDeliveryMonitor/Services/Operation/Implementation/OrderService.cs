using Newtonsoft.Json;
using OrderDeliveryMonitor.Facade.Implementation.Operation.DTO;
using OrderDeliveryMonitor.Services.Operation.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrderDeliveryMonitor.Services.Operation.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient pHttpClient)
        {
            _httpClient = pHttpClient;
        }

        public async Task<OrderDTO> Get(string pOrderCode)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{_httpClient.BaseAddress}/{pOrderCode}");
                var content = JsonConvert.DeserializeObject<OrderDTO>(response);

                return content;
            }
            catch(HttpRequestException ex)
            {
                //TODO: Log.
                return new OrderDTO();
            }
            catch (Exception ex)
            {
                //TODO: Log.
                return new OrderDTO();
            }
        }

        public async Task<List<OrderDTO>> GetAll()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_httpClient.BaseAddress);
                var content = JsonConvert.DeserializeObject<List<OrderDTO>>(response);

                return content;
            }            
            catch (HttpRequestException ex)
            {
                //TODO: Log.
                return new List<OrderDTO>();
            }
            catch (Exception ex)
            {
                //TODO: Log.
                return new List<OrderDTO>();
            }
        }

        public async Task PutInLine(string pOrderCode)
        {
            try
            {
                string vOrderCode = JsonConvert.SerializeObject(new { OrderCode = pOrderCode });

                StringContent content = 
                    new StringContent(
                            vOrderCode, 
                            Encoding.UTF8, 
                            "application/json"
                        );

                await _httpClient.PutAsync(_httpClient.BaseAddress, content);
            }
            catch(HttpRequestException ex)
            {
                //TODO: Log.
            }
            catch(Exception ex)
            {
                //TODO: Log.
            }
        }
    }
}
