using alwaysinformed_dal.Data;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace alwaysinformed_dal.Repositories
{
    public class ArticleStatisticRepository : IArticleStatisticRepository
    {
        private readonly AidbContext context;

        public ArticleStatisticRepository(AidbContext context)
        {
            this.context = context;
        }
        public async Task<ArticleStatistic?> AddAsync(ArticleStatistic entity)
        {
            await context.ArticleStatistics.AddAsync(entity);
            await context.SaveChangesAsync(true);
            return await context.ArticleStatistics.FirstOrDefaultAsync(a => a.ArticleId == entity.ArticleId);
        }

        public async void DeleteAsync(ArticleStatistic entity)
        {
            context.ArticleStatistics.Remove(entity);
            await context.SaveChangesAsync(true);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var e = context.ArticleStatistics.FirstOrDefault(d => d.ArticleId == id) ?? throw new NullReferenceException();
            context.ArticleStatistics.Remove(e);
            await context.SaveChangesAsync(true);
        }

        public async Task<List<ArticleStatistic>> GetAllAsync()
        {
            return await context.ArticleStatistics.ToListAsync();
        }

        public async Task<ArticleStatistic?> GetByArticleIdAsync(int articleId)
        {
            return await context.ArticleStatistics.FirstOrDefaultAsync(a => a.ArticleId == articleId);
        }

        public async Task<ArticleStatistic> GetByIdAsync(int id)
        {
            return await context.ArticleStatistics.FirstOrDefaultAsync(a => a.StatisticId == id);
        }

        public async Task<List<ArticleStatistic>> GetByUserIdAsync(int userId)
        {
            var entities = from aStat in context.ArticleStatistics
                           join a in context.Articles on aStat.ArticleId equals a.ArticleId
                           join au in context.Authors on a.AuthorId equals au.AuthorId
                           join u in context.Users on au.UserId equals u.UserId
                           where au.UserId == userId
                           select aStat;

            return await entities.ToListAsync();
        }

        public async Task IncreaseLikesByArticleId(int articleId)
        {
            var entity = await context.ArticleStatistics.FirstOrDefaultAsync(a => a.ArticleId == articleId) ?? throw new NullReferenceException();
            entity.Likes++;
            context.SaveChanges(true);
        }

        public async Task IncreaseLikesByStatisticId(int statisticId)
        {
            var entity = await context.ArticleStatistics.FindAsync(statisticId) ?? throw new NullReferenceException();
            entity.Likes++;
            context.SaveChanges(true);
        }

        public async Task IncreaseViewsByArticleId(int articleId)
        {
            var entity = await context.ArticleStatistics.FirstOrDefaultAsync(a => a.ArticleId == articleId) ?? throw new NullReferenceException();
            entity.Views++;
            context.SaveChanges(true);
        }

        public async Task IncreaseViewsByStatisticId(int statisticId)
        {
            var entity = await context.ArticleStatistics.FindAsync(statisticId) ?? throw new NullReferenceException();
            entity.Views++;
            context.SaveChanges(true);
        }

        public async Task<ArticleStatistic> Update(ArticleStatistic entity)
        {
            context.ArticleStatistics.Update(entity);
            await context.SaveChangesAsync(true);
            return await context.ArticleStatistics.FindAsync(entity.StatisticId);
        }
    }
}
