using Domain.Models;
using Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Reposatories
{
    public class ProductSpecification:Specifications<Product>
    {

        public ProductSpecification(int id) : base(p=>p.Id == id)
        {
            base.GetIncludes(p => p.ProductBrand);
            base.GetIncludes(p => p.ProductType);
        }

        public ProductSpecification(ProductQueryPatameter ProductQueryPatameter) :base(CreateCriteria(ProductQueryPatameter))
        {
            base.GetIncludes(p => p.ProductBrand);
            base.GetIncludes(p => p.ProductType);

            ApplySorting(ProductQueryPatameter);

            base.ApplyPagination(ProductQueryPatameter.pagesize, ProductQueryPatameter.pageindex);


        }

        private static Expression<Func<Product,bool>> CreateCriteria(ProductQueryPatameter ProductQueryPatameter)
        {
            return p =>
            (!ProductQueryPatameter.Brandid.HasValue || ProductQueryPatameter.Brandid.Value == p.BrandId)
            &&
            (!ProductQueryPatameter.Typeid.HasValue || ProductQueryPatameter.Typeid.Value == p.TypeId)
            &&
            (string.IsNullOrWhiteSpace(ProductQueryPatameter.SearchValue) || p.Name.ToLower().Contains(ProductQueryPatameter.SearchValue.ToLower()));
        }

        private void ApplySorting(ProductQueryPatameter ProductQueryPatameter)
        {
            switch (ProductQueryPatameter.ProductSortingOptions)
            {
                case ProductSortingOptions.Price:
                    AddOrderBy(p => p.Price); break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price); break;

                case ProductSortingOptions.Name:
                    AddOrderBy(p => p.Name); break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name); break;

            }
        }
    }
}
