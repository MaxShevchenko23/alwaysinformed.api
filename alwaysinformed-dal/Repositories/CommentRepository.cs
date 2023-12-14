using alwaysinformed_dal.Data;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace alwaysinformed_dal.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AidbContext context;

        public CommentRepository(AidbContext context)
        {
            this.context = context;
        }
        public async Task<Comment> AddAsync(Comment entity)
        {
            await context.Comments.AddAsync(entity);
            await context.SaveChangesAsync(true);
            var added = await context.Comments.FirstAsync(a=>a.Text == entity.Text);
            return added;
        }

        public void DeleteAsync(Comment entity)
        {
            context.Comments.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await context.Comments.FirstOrDefaultAsync(x => x.CommentId == id) ?? throw new NullReferenceException();
            context.Comments.Remove(entity);
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await context.Comments.ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await context.Comments.FirstOrDefaultAsync(x => x.CommentId == id) ?? throw new ArgumentNullException();
        }

        public async Task<List<Comment>> GetCommentByArticleId(int articleId)
        {
            return await context.Comments.Where(c => c.ArticleId == articleId).ToListAsync() ?? throw new ArgumentNullException();
        }

        public async Task<List<Comment>> GetFirstRecords(int amount)
        {
            return await context.Comments.Where(d => d.CommentId <= amount).ToListAsync();
        }

        public async Task<List<Comment>> GetLastRecords(int amount)
        {
            return await context.Comments.OrderByDescending(m => m.CommentId).Take(amount).ToListAsync();
        }

        public async Task<Comment> Update(Comment entity)
        {
            context.Comments.Update(entity);
            await context.SaveChangesAsync(true);
            var updated = await context.Comments.FirstAsync(a => a.Text == entity.Text);
            return updated;
        }
    }
}
