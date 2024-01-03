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
        public async Task<Article> AddAsync(Article entity)
        {
            await context.Articles.AddAsync(entity);
            await context.SaveChangesAsync(true);
            return await context.Articles.FirstOrDefaultAsync(a => a.Url == entity.Url);
        }

        public async Task AddFromSandbox(Article article)
        {
            await context.Articles.AddAsync(article);
        }

        public async Task DeleteArticleBySandboxId(int sandboxId)
        {
            var article = await context.Articles.FirstAsync(a => a.ArticleSandboxId == sandboxId) ?? throw new AiDbException();
            context.Remove(article);
        }

        public async void DeleteAsync(Article entity)
        {
            context.Articles.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var e = context.Articles.FirstOrDefault(d => d.ArticleId == id) ?? throw new ArgumentNullException();
            context.Articles.Remove(e);
        }

        public async Task<List<Article>> SearchArticles(string? search)
        {
            var articles = context.Articles as IQueryable<Article>;

            search = search.Trim();

            if (!string.IsNullOrEmpty(search))
            {
                articles = articles.Where(a => a.Title.Contains(search)
                || a.Content.Contains(search)
                || a.ShortDescription.Contains(search)
                || a.Category.CategoryName.Contains(search));
            }

            return await articles.ToListAsync();
        }

        public async Task<List<Article>> GetAllAsync()
        {
            var articles = from a in context.Articles
                           join s in context.ArticleSandboxes on a.ArticleSandboxId equals s.SandboxId
                           where s.ArticleStatus == 2
                           select a;

            return await articles.ToListAsync();
        }
        public async Task<IQueryable<Article>> GetAllQueryableAsync()
        {
            var articles = from a in context.Articles
                           join s in context.ArticleSandboxes on a.ArticleSandboxId equals s.SandboxId
                           where s.ArticleStatus == 2
                           select a;

            return articles;
        }
        public async Task<Article?> GetArticleByArticleSandboxId(int articleSandboxId)
        {
            return await context.Articles.FirstOrDefaultAsync(c => c.ArticleSandboxId == articleSandboxId);
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
            //статьи должны быть с привязанными articlesandboxid
            return await context.Articles.Where(a => a.ArticleSandboxId > 0).Take(amount).ToListAsync();
        }

        public async Task<List<Article>> GetLastRecords(int amount)
        {
            //статьи должны быть с привязанными articlesandboxid
            return await context.Articles.Where(a => a.ArticleSandboxId > 0).OrderByDescending(m => m.ArticleId).Take(amount).ToListAsync();
        }

        public async Task<Article> Update(Article entity)
        {
            context.Articles.Update(entity);
            await context.SaveChangesAsync(true);
            var updatedArticle = await context.Articles.FirstOrDefaultAsync(a => a.Url == entity.Url);
            return updatedArticle;
        }

        public async Task<List<Article>> FilterByCategory(string? categoryName)
        {
            var articles = context.Articles;
            categoryName = categoryName.Trim();

            return await articles.Where(a => a.Category.CategoryName == categoryName).ToListAsync();
        }

        public async Task<List<Article>> FilterByAuthor(string? firstName, string? lastName)
        {
            var articles = context.Articles;
            firstName = firstName.Trim();
            lastName = lastName.Trim();

            return await articles.Where(a => a.Author.FirstName.Contains(firstName) || a.Author.LastName.Contains(lastName)).ToListAsync();
        }

        public async Task<List<Article>> FilterByDate(DateTime? date)
        {
            var articles = context.Articles;
            return await articles.Where(a => a.PublicationDate.Date == date).ToListAsync();
        }
    }
}
