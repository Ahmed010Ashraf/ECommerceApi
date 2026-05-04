using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public static class SpecificationEvaluttor<T> where T : class
    {
        public static IQueryable<T>  GenerateQuery(IQueryable<T> basequery, ISpecifications<T> specs) 
        {
            var query = basequery;
            if (specs.Criteria != null) {
                query = query.Where(specs.Criteria);
            }

            if (specs.OrderBy != null) { query = query.OrderBy(specs.OrderBy); }

            if(specs.OrderByDesc != null) { query = query.OrderByDescending(specs.OrderByDesc); }

            if (specs.Includes != null) {

                query = specs.Includes.Aggregate(query, (cur, acc) => cur.Include(acc));

                //foreach (var include in specs.Includes) { 
                //query = query.Include(include);
                //}

            }

            if (specs.ispagenated)
            {
                query = query.Skip(specs.skip).Take(specs.take);
            }

            return query;
        }

     
    }
}
