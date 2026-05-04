using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Presistance.Reposatories;
using ServiceImplementation.specifications;
using ServicesAbstraction;
using Shared;
using Shared.DTOs.ProductDTOs;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class ProductService(IUniteOfWork _unitofwork ,IMapper _mapper ) : IProductServices
    {
     

        public async Task<IEnumerable<ProductResponse>> GetAllProducts(ProductQueryPatameter ProductQueryPatameter)
        {
            //var prroducts = await _unitofwork.GetRepository<Product, int>().GetAll();
            //var productResponses = _mapper.Map<IEnumerable<ProductResponse>>(prroducts);
            //return productResponses;


            var productspecs = new ProductSpecification(ProductQueryPatameter);

            var products = await _unitofwork.GetRepository<Product, int>().GetAll(productspecs);

            var productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return productResponses;
        }

       

        public async Task<ProductResponse> GetProductById(int id)
        {
            //var product =await _unitofwork.GetRepository<Product, int>().GetByIdAsync(id);
            //var productResponse = _mapper.Map<ProductResponse>(product);
            //return productResponse;

            var productspecs = new ProductSpecification(id);

            var product =  await _unitofwork.GetRepository<Product , int>().GetByIdAsync(productspecs) ?? 
                throw new ProductNotFoundException(id);

            var productResponse = _mapper.Map<ProductResponse>(product);

            return productResponse;
            
        }


        public async Task<IEnumerable<BrandResponse>> GetAllBrands()
        {
            var repo = _unitofwork.GetRepository<ProductBrand, int>();
            var brands = await repo.GetAll();
            var brandResponses = _mapper.Map<IEnumerable<BrandResponse>>(brands);
            return brandResponses;
        }


        public async Task<IEnumerable<TypeResponse>> GetAllTypes()
        {
            var repo = _unitofwork.GetRepository<ProductType, int>();
            var types = await repo.GetAll();
            var typeResponses = _mapper.Map<IEnumerable<TypeResponse>>(types);
            return typeResponses;
        }

        public async Task<int> ProductCount(ProductQueryPatameter ProductQueryPatameter)
        {
            var specs = new ProductWithCountSpecs(ProductQueryPatameter);
            return await _unitofwork.GetRepository<Product, int>().ProductCount(specs);
        }
    }
}
