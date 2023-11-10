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
    public class ArticleSandboxRepository : IArticleSandboxRepository
    {
        private readonly AidbContext context;

        public ArticleSandboxRepository(AidbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(ArticleSandbox entity)
        {
           await context.AddAsync(entity);
        }

        public void Delete(ArticleSandbox entity)
        {
            context.ArticleSandboxes.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await context.ArticleSandboxes.FirstOrDefaultAsync(x => x.ArticleId == id) ?? throw new ArgumentNullException();
            context.ArticleSandboxes.Remove(entity);
        }

        public async Task<List<ArticleSandbox>> GetAllAsync()
        {
            return await context.ArticleSandboxes.ToListAsync();
        }

        public async Task<ArticleSandbox> GetByIdAsync(int id)
        {
            return await context.ArticleSandboxes.FirstOrDefaultAsync(x => x.ArticleId == id);
        }

        public async Task<List<ArticleSandbox>> GetFirstRecords(int amount)
        {
            return await context.ArticleSandboxes.Where(d => d.ArticleId <= amount).ToListAsync();
        }

        public async Task<List<ArticleSandbox>> GetLastRecords(int amount)
        {
            return await context.ArticleSandboxes.OrderByDescending(m => m.ArticleId).Take(amount).ToListAsync();
        }

        public void Update(ArticleSandbox entity)
        {
            context.ArticleSandboxes.Update(entity);
        }
    }
}
