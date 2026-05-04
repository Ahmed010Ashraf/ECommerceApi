using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public enum PaymentStatus
    {
        Pending=0,
        PaymentReceived=1,
        Failed=2
    }
    public class Order:BaseEntity<Guid>
    {
        public string UserEmail { get; set; }
        public List<OrderItems> Items { get; set; } = [];

        public OrderAddress Address { get; set; }

        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal Subtotal { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public DateTimeOffset Date { get; set;  } = DateTimeOffset.Now;

        public DelivaryMethod DelivaryMethod { get; set; }

        public Guid DelivaryMethodId { get; set; }
    }
}
