using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
       
        public string PictureUrl { get; set; } = default!;

        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }

        public int TypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
