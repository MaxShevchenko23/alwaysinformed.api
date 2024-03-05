using alwaysinformed_dal.Data;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace alwaysinformed_dal.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AidbContext context;

        public FavoriteRepository(AidbContext context)
        {
            this.context = context;
        }
        public async Task<Favorite> AddAsync(Favorite entity)
        {
            var check = await context.Favorites.FirstOrDefaultAsync(a=>a.UserId== entity.UserId && a.ArticleId==entity.ArticleId);

            if (check != null) 
                return check;

            await context.Favorites.AddAsync(entity);
            await context.SaveChangesAsync(true);

            var added = await context.Favorites.Where(f => f.UserId == entity.UserId).FirstAsync();
            return added;
        }

        public void DeleteAsync(Favorite entity)
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

        public async Task<IEnumerable<Article>?> GetByUserId(int userId)
        {
            var favs = await context.Favorites.Where(f => f.UserId == userId).Select(f=>f.ArticleId).ToArrayAsync();
            var favsArticles = await context.Articles.Where(a => favs.Contains(a.ArticleId)).ToListAsync();
            return favsArticles;
        }

        public async Task<Favorite> Update(Favorite entity)
        {
            context.Favorites.Update(entity);
            await context.SaveChangesAsync(true);
            var updated = await context.Favorites.FirstAsync(a => a.ArticleId == entity.ArticleId && a.UserId == entity.UserId);
            return updated;
        }
    }
}
