using alwaysinformed_dal.Data;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_dal.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AidbContext context;

        public CommentRepository(AidbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Comment entity)
        {
            await context.Comments.AddAsync(entity);
        }

        public void Delete(Comment entity)
        {
            context.Comments.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await context.Comments.FirstOrDefaultAsync(x => x.CommentId == id) ?? throw new ArgumentNullException();
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

        public async Task<List<Comment>> GetFirstRecords(int amount)
        {
            return await context.Comments.Where(d => d.CommentId <= amount).ToListAsync();
        }

        public async Task<List<Comment>> GetLastRecords(int amount)
        {
            return await context.Comments.OrderByDescending(m => m.CommentId).Take(amount).ToListAsync();
        }

        public void Update(Comment entity)
        {
            context.Comments.Update(entity);
        }
    }
}
