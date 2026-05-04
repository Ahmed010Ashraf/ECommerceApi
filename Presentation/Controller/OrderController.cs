using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IServiceManager _serviceManager):ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> Create(OrderRequest orderReuqest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var res = await _serviceManager.IOrderService.Create(email,orderReuqest);
            return Ok(res);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<OrderResponse>> GetOrderById (Guid id)
        {
            return Ok( await _serviceManager.IOrderService.GetOrderByIdAsync(id));
        }

        [HttpGet("DelivaryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResponse>>> GetDeliveryMethodS()
        {
            return Ok(await _serviceManager.IOrderService.GetDeliveryMethodsAsync());
        }


        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAllOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManager.IOrderService.GetAllOrdersAsync(email));
        }
    }
}
