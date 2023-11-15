using alwaysinformed_dal.Data;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async void DeleteAsync(ArticleSandbox entity)
        {
            
            context.ArticleSandboxes.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await context.ArticleSandboxes.FirstOrDefaultAsync(x => x.SandboxId == id) ?? throw new ArgumentNullException();
            //КАСТЫЛЬ ALERT
            ArticleRepository rep = new(context);
            await rep.DeleteArticleBySandboxId(entity.SandboxId);

            context.ArticleSandboxes.Remove(entity);
        }

        public async Task<List<ArticleSandbox>> GetAllAsync()
        {
            return await context.ArticleSandboxes.ToListAsync();
        }

        public async Task<List<ArticleSandbox>> GetByAuthorId(int authorId)
        {
            return await context.ArticleSandboxes.Where(a => a.AuthorId == authorId).ToListAsync();
        }

        public async Task<ArticleSandbox?> GetByIdAsync(int id)
        {
            return await context.ArticleSandboxes.FirstOrDefaultAsync(x => x.SandboxId == id);
        }
        public async Task<ArticleSandbox?> GetByURLAsync(string url)
        {
            return await context.ArticleSandboxes.FirstOrDefaultAsync(x => x.Url == url);
        }

        public async Task<List<ArticleSandbox>> GetByUserId(int id)
        {
            var author = await context.Authors.FirstAsync(c => c.UserId == id);
            return await context.ArticleSandboxes.Where(a => a.AuthorId == author.AuthorId).ToListAsync();

        }

        public async Task<List<ArticleSandbox>> GetFirstRecords(int amount)
        {
            return await context.ArticleSandboxes.Where(d => d.SandboxId <= amount).ToListAsync();
        }

        public async Task<List<ArticleSandbox>> GetLastRecords(int amount)
        {
            return await context.ArticleSandboxes.OrderByDescending(m => m.SandboxId).Take(amount).ToListAsync();
        }

        public void Update(ArticleSandbox entity)
        {
            context.ArticleSandboxes.Update(entity);
        }
    }
}
