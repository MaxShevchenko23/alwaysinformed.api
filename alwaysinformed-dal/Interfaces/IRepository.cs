using alwaysinformed_dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_dal.Interfaces
{
    public interface IRepository<TEntity>
    {
        public Task<List<TEntity>> GetAllAsync();
        public Task <TEntity> GetByIdAsync(int id);        
        
        //Task<List<TEntity>> GetFirstRecords(int amount);
        
        //Task<List<TEntity>> GetLastRecords(int amount);

        public Task<TEntity> AddAsync(TEntity entity);
        public void DeleteAsync(TEntity entity);

        public Task DeleteByIdAsync(int id);

        public Task<TEntity> Update(TEntity entity);
    }
}
