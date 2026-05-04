using Shared;
using Shared.DTOs.ProductDTOs;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IProductServices
    {
        public Task<IEnumerable<ProductResponse>> GetAllProducts(ProductQueryPatameter productQueryPatameter);

        public Task<ProductResponse> GetProductById(int id);

        public Task<IEnumerable<BrandResponse>> GetAllBrands();

        public Task<IEnumerable<TypeResponse>> GetAllTypes();

        public Task<int> ProductCount(ProductQueryPatameter ProductQueryPatameter);
    }
}
