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
    public class UserRepository : IUserRepository
    {
        private readonly AidbContext context;

        public UserRepository(AidbContext context)
        {
            this.context = context;
        }
        public async Task<User> AddAsync(User entity)
        {
            await context.Users.AddAsync(entity);
            await context.SaveChangesAsync(true);
            var added = await context.Users.Where(c=>c.Username == entity.Username).FirstOrDefaultAsync();
            return added;
        }

        public void DeleteAsync(User entity)
        {
            context.Users.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await context.Users.FirstOrDefaultAsync(x => x.UserId == id) ?? throw new ArgumentNullException();
            context.Users.Remove(entity);

        }

        public async Task<List<User>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<List<User>> GetFirstRecords(int amount)
        {
            return await context.Users.Where(d => d.UserId <= amount).ToListAsync();
        }

        public async Task<List<User>> GetLastRecords(int amount)
        {
            return await context.Users.OrderByDescending(m => m.UserId).Take(amount).ToListAsync();
        }

        public async Task<User> Update(User entity)
        {
            context.Users.Update(entity);
            await context.SaveChangesAsync(true);
            var updated = await context.Users.Where(u => u.UserId == entity.UserId).FirstOrDefaultAsync();
            return updated;
        }
    }
}
