using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecifications<T>where T : class
    {
         Expression<Func<T, bool>> Criteria { get;  }
         List<Expression<Func<T, object>>> Includes { get; }

        Expression<Func<T,object>> OrderBy { get; }
        Expression<Func<T,object>> OrderByDesc { get; }
       

        public void GetIncludes(Expression<Func<T, object>> include);


        public int skip { get; }
        public int take { get; }

        public bool ispagenated { get; }
    }
}
