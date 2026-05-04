using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;
using ServiceImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Reposatories
{
    public class GenericReposatory<TEntity, TKey>(StoreDbContext _storedbcontext) :
        IGenericReposatory<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity)
        {
            _storedbcontext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _storedbcontext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _storedbcontext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _storedbcontext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _storedbcontext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity> specs)
        {
            return await SpecificationEvaluttor<TEntity>.GenerateQuery(_storedbcontext.Set<TEntity>() , specs).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(ISpecifications<TEntity> specs)
        {
            return await SpecificationEvaluttor<TEntity>.GenerateQuery(_storedbcontext.Set<TEntity>(), specs).ToListAsync();
        }

        public async Task<int> ProductCount(ISpecifications<TEntity> specs)
        {
            return await SpecificationEvaluttor<TEntity>.GenerateQuery(_storedbcontext.Set<TEntity>(),specs).CountAsync();
        }
    }
}
