using Domain.Models;
using Domain.Models.Order;
using Presistance.Reposatories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation.specifications
{
    public class OrderSpecification : Specifications<Order>
    {
        public OrderSpecification(Guid id):base(o=>o.Id ==  id)
        {
            GetIncludes(o => o.Items);
            GetIncludes(o => o.DelivaryMethod);
        }


        public OrderSpecification(string email) : base(o => o.UserEmail == email)
        {
            GetIncludes(o => o.Items);
            GetIncludes(o => o.DelivaryMethod);
        }
    }
}
