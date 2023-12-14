using alwaysinformed_dal.Data;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace alwaysinformed_dal.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AidbContext context;

        public CategoryRepository(AidbContext context)
        {
            this.context = context;
        }
        public async Task<Category> AddAsync(Category entity)
        {
            await context.Categories.AddAsync(entity);
            await context.SaveChangesAsync(true);
            return await context.Categories.FirstOrDefaultAsync(c => c.CategoryName == entity.CategoryName);
        }

        public void DeleteAsync(Category entity)
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

        public async Task<Category> Update(Category entity)
        {
            context.Categories.Update(entity);
            await context.SaveChangesAsync(true);

            var updated = await context.Categories.FirstOrDefaultAsync(c => c.CategoryName == entity.CategoryName);
            return updated;
        }

    }
}
