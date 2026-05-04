using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Reposatories
{
    public abstract class Specifications<T> : ISpecifications<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get ; protected set; }

        public List<Expression<Func<T, object>>> Includes { get; } = [];

        public Expression<Func<T, object>> OrderBy { get; protected set; }

        public void AddOrderBy(Expression<Func<T,object>> orderby)
        {
            OrderBy = orderby;
        }
        public void AddOrderByDesc(Expression<Func<T,object>> orderby)
        {
            OrderByDesc = orderby;
        }

        public Expression<Func<T, object>> OrderByDesc { get; protected set; }

        public int skip{ get; protected set; }

        public int take { get; protected set; }

        public bool ispagenated { get; protected set; }


        public void ApplyPagination(int pagesize , int pageindex)
        {
            ispagenated = true;
            take = pagesize;
            skip = (pageindex-1)*pagesize;
        }

        protected Specifications(Expression<Func<T,bool>> _criteria)
        {
            Criteria = _criteria;
        }
        public void GetIncludes(Expression<Func<T, object>> include)
        {
            Includes.Add(include);
        }


        
    }
}
