using alwaysinformed_dal.Data;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_dal.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AidbContext context;

        public CategoryRepository(AidbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Category entity)
        {
            await context.Categories.AddAsync(entity);
        }

        public void Delete(Category entity)
        {
            context.Categories.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id) ?? throw new ArgumentNullException();
            context.Categories.Remove(entity);

        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id) ?? throw new ArgumentNullException();
        }

        public async Task<List<Category>> GetFirstRecords(int amount)
        {
            return await context.Categories.Where(d => d.CategoryId <= amount).ToListAsync();
        }

        public async Task<List<Category>> GetLastRecords(int amount)
        {
            return await context.Categories.OrderByDescending(m => m.CategoryId).Take(amount).ToListAsync();
        }

        public void Update(Category entity)
        {
            context.Categories.Update(entity);
        }
    }
}
