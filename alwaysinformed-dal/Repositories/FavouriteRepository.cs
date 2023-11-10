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
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AidbContext context;

        public FavoriteRepository(AidbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Favorite entity)
        {
            await context.Favorites.AddAsync(entity);
        }

        public void Delete(Favorite entity)
        {
            context.Favorites.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await context.Favorites.FirstOrDefaultAsync(x => x.FavoriteId == id) ?? throw new ArgumentNullException();
            context.Favorites.Remove(entity);

        }

        public async Task<List<Favorite>> GetAllAsync()
        {
            return await context.Favorites.ToListAsync();
        }

        public async Task<Favorite> GetByIdAsync(int id)
        {
            return await context.Favorites.FirstOrDefaultAsync(x => x.FavoriteId == id) ?? throw new ArgumentNullException();
        }

        public async Task<List<Favorite>> GetFirstRecords(int amount)
        {
            return await context.Favorites.Where(d => d.FavoriteId <= amount).ToListAsync();
        }

        public async Task<List<Favorite>> GetLastRecords(int amount)
        {
            return await context.Favorites.OrderByDescending(m => m.FavoriteId).Take(amount).ToListAsync();
        }

        public void Update(Favorite entity)
        {
            context.Favorites.Update(entity);
        }
    }
}
