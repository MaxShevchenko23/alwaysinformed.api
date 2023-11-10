using alwaysinformed_dal.Data;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace alwaysinformed_dal.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AidbContext context;

        public ArticleRepository(AidbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Article entity)
        {
            await context.Articles.AddAsync(entity);
        }

        public async Task AddFromSandbox(Article article)
        {
            await context.Articles.AddAsync(article);
        }

        public async void Delete(Article entity)
        {
            context.Articles.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var e = context.Articles.FirstOrDefault(d => d.ArticleId == id) ?? throw new ArgumentNullException();
            context.Articles.Remove(e);
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await context.Articles.ToListAsync();
        }

        public async Task<Article> GetArticleByURL(string url)
        {
            return await context.Articles.FirstAsync(d => d.Url == url);
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await context.Articles.FirstOrDefaultAsync(d => d.ArticleId == id);
        }

        public async Task<List<Article>> GetFirstRecords(int amount)
        {
            return await context.Articles.Take(amount).ToListAsync();
        }

        public async Task<List<Article>> GetLastRecords(int amount)
        {
            return await context.Articles.OrderByDescending(m => m.ArticleId).Take(amount).ToListAsync();
        }

        public void Update(Article entity)
        {
            context.Articles.Update(entity);
        }
    }
}
