using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DTOs.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BasketController(IServiceManager _serviceManager):ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id)
        {
            var basket = await _serviceManager.IBasketService.GetBasketAsync(id);
            return Ok(basket);
        }


        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdateBasket(CustomerBasketDto basket)
        {
            var updatedbasket = await _serviceManager.IBasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(updatedbasket);
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            var isdeleted = await _serviceManager.IBasketService.DeleteBasketAsync(id);
            if (!isdeleted) return BadRequest(new { message = "problem in deleting basket" });
            return Ok(true);
        }


    }
}
