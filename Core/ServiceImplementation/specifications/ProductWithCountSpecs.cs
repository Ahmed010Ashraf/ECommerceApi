using Domain.Models;
using Presistance.Reposatories;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.specifications
{
    public class ProductWithCountSpecs(ProductQueryPatameter ProductQueryPatameter): Specifications<Product>(CreateCriteria(ProductQueryPatameter))
    {
      

        private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryPatameter ProductQueryPatameter)
        {
            return p =>
            (!ProductQueryPatameter.Brandid.HasValue || ProductQueryPatameter.Brandid.Value == p.BrandId)
            &&
            (!ProductQueryPatameter.Typeid.HasValue || ProductQueryPatameter.Typeid.Value == p.TypeId)
            &&
            (string.IsNullOrWhiteSpace(ProductQueryPatameter.SearchValue) || p.Name.ToLower().Contains(ProductQueryPatameter.SearchValue.ToLower()));
        }
    }
}
