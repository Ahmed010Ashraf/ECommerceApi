using Shared.DTOs.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.OrderDtos
{
    public class OrderRequest
    {
        public string BasketId { get; set; }
        public AddressDto Address { get; set; }

        public Guid DelivaryMethodId { get; set; }
    }
}
