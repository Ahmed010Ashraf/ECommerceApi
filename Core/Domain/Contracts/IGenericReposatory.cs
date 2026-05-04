using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericReposatory<TEntity ,TKey>where TEntity :BaseEntity<TKey>
    {

        public void Add(TEntity entity);

        public void Update(TEntity entity);

        public void Delete(TEntity entity);

        public Task<TEntity?> GetByIdAsync(TKey id);

        public Task<IEnumerable<TEntity>> GetAll();
        public Task<TEntity?> GetByIdAsync(ISpecifications<TEntity> specs);

        public Task<IEnumerable<TEntity>> GetAll(ISpecifications<TEntity> specs);
        public Task<int> ProductCount(ISpecifications<TEntity> specs);
    }
}
