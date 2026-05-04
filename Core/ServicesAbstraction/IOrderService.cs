using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IOrderService
    {
        public Task<OrderResponse> Create(string userEmail, OrderRequest orderRequest);

        public Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync();

        public Task<OrderResponse> GetOrderByIdAsync(Guid Id);

        public Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(string userEmail);
    }
}
