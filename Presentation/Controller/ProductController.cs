using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared;
using Shared.DTOs.ProductDTOs;
using Shared.Enums;
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
    public class ProductController(IServiceManager _servicemanager):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagenatedResponse<ProductResponse>>> GetAllProducts([FromQuery]ProductQueryPatameter ProductQueryPatameter)
        {
            var products = await _servicemanager.IProductServices.GetAllProducts(ProductQueryPatameter);
            var ProductCount = await _servicemanager.IProductServices.ProductCount(ProductQueryPatameter);
            var pagenated = new PagenatedResponse<ProductResponse>()
            {
                PageIndex = ProductQueryPatameter.pageindex,
                PageSize = ProductQueryPatameter.pagesize,
                TotalCount = ProductCount,
                Data = products
            };
            return Ok(pagenated);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(int id)
        {
            var product = await  _servicemanager.IProductServices.GetProductById(id);
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandResponse>>> GetAllBrands()
        {
            var brands = await _servicemanager.IProductServices.GetAllBrands();
            return Ok(brands);
        }

        [HttpGet("types")]  
        public async Task<ActionResult<IEnumerable<TypeResponse>>> GetAllTypes()
        {
            var types = await _servicemanager.IProductServices.GetAllTypes();
            return Ok(types);
        }

    }
}
