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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AidbContext context;

        public AuthorRepository(AidbContext context)
        {
            this.context = context;
        }
        public async Task<Author> AddAsync(Author entity)
        {
            await context.Authors.AddAsync(entity);
            await context.SaveChangesAsync(true);
            var added = await context.Authors.Where(a => a.FirstName == entity.FirstName && a.LastName == entity.LastName).FirstAsync();
            return added;
        }

        public void DeleteAsync(Author entity)
        {
            context.Authors.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await context.Authors.FirstOrDefaultAsync(x => x.AuthorId == id) ?? throw new ArgumentNullException();
            context.Authors.Remove(entity);
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await context.Authors.ToListAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await context.Authors.FirstOrDefaultAsync(x => x.AuthorId == id) ?? throw new ArgumentNullException();
        }

        public async Task<List<Author>> GetFirstRecords(int amount)
        {
            return await context.Authors.Where(d => d.AuthorId <= amount).ToListAsync();
        }

        public async Task<List<Author>> GetLastRecords(int amount)
        {
            return await context.Authors.OrderByDescending(m => m.AuthorId).Take(amount).ToListAsync();
        }

        public async Task<Author> Update(Author entity)
        {
            context.Authors.Update(entity);
            await context.SaveChangesAsync(true);
            var added = await context.Authors.Where(a => a.FirstName == entity.FirstName && a.LastName == entity.LastName).FirstAsync();
            return added;
        }
    }
}
