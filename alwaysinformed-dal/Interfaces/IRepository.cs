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

        public Task AddAsync(TEntity entity);
        public void Delete(TEntity entity);

        public Task DeleteByIdAsync(int id);

        public void Update(TEntity entity);
    }
}
