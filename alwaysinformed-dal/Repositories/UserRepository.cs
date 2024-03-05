using alwaysinformed_dal.Data;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace alwaysinformed_dal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AidbContext context;

        public UserRepository(AidbContext context)
        {
            this.context = context;
        }
        public async Task<User?> AddAsync(User entity)
        {
<<<<<<< HEAD
            var entry = await context.Users.FirstOrDefaultAsync(u => u.Username == entity.Username && u.PasswordHash == entity.PasswordHash);
=======
            var entry = await context.Users.FirstOrDefaultAsync(u=>u.Username == entity.Username && u.PasswordHash == entity.PasswordHash);
>>>>>>> token
            if (entry == null)
            {
                await context.Users.AddAsync(entity);
                await context.SaveChangesAsync(true);
<<<<<<< HEAD
                var added = await context.Users.Where(c => c.Username == entity.Username).FirstOrDefaultAsync();
                return added;
=======
                var added = await context.Users.Where(c=>c.Username == entity.Username).FirstOrDefaultAsync();
                return added; 
>>>>>>> token
            }
            return null;
        }

        public void DeleteAsync(User entity)
        {
            context.Users.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
<<<<<<< HEAD
            context.Favorites.Where(u => u.UserId == id)
                .ExecuteDelete();

            context.Users.Where(u => u.UserId == id)
                .ExecuteUpdate(s => s.SetProperty(u => u.Username, u => u.Username + "User"));


            context.Users.Where(u => u.UserId == id).ExecuteDelete();
=======
            var entity = await context.Users.FirstOrDefaultAsync(x => x.UserId == id) ?? throw new ArgumentNullException();
            context.Users.Remove(entity);

>>>>>>> token
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<User?> GetByNameAndPassword(string username, string password)
        {
            return await context.Users.FirstOrDefaultAsync(a => a.Username == username && a.PasswordHash == password);
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
