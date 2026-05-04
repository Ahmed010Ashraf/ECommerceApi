using Domain.Contracts;
using Domain.Models;
using Presistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Reposatories
{
    public class UniteOfWork(StoreDbContext _storedbcontext): IUniteOfWork
    {

        private readonly Dictionary<string, object> GenericReposatorys = []; 
        public IGenericReposatory<TEntity1, TKey1> GetRepository<TEntity1, TKey1>() where TEntity1 : BaseEntity<TKey1>
        {
            var typename = typeof(TEntity1).Name;
            if(GenericReposatorys.ContainsKey(typename))
            {
                return (GenericReposatory<TEntity1, TKey1>)GenericReposatorys[typename];
            }

            var repo = new GenericReposatory<TEntity1, TKey1>(_storedbcontext);
            GenericReposatorys.Add(typename, repo);
            return repo;    
        }

        public async Task<int> SavechangesAsync()
        {
            return await _storedbcontext.SaveChangesAsync();
        }
    }
}
