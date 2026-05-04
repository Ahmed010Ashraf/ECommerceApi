using Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.OrderDtos
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public List<OrderItemDto> Items { get; set; } = [];

        public AddressDto Address { get; set; }

        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        public string PaymentStatus { get; set; } 
        public DateTimeOffset Date { get; set; } 

        public string DelivaryMethod { get; set; }

    }

    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
