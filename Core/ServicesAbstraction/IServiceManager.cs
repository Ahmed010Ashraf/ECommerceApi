using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IServiceManager
    {
        public IProductServices IProductServices { get; }
        public IBasketService IBasketService { get; }
        public IAuthenticationService IAuthenticationService { get; }
        public IOrderService IOrderService { get; }
    }
}
