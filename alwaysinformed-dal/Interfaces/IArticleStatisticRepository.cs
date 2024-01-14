using alwaysinformed_dal.Entities;

namespace alwaysinformed_dal.Interfaces
{
    public interface IArticleStatisticRepository:IRepository<ArticleStatistic>
    {
        Task<List<ArticleStatistic>> GetByUserIdAsync(int userId);
        Task<ArticleStatistic> GetByArticleIdAsync(int articleId);
        Task IncreaseViewsByArticleId(int articleId);
        Task IncreaseViewsByStatisticId(int statisticId);
        Task IncreaseLikesByArticleId(int articleId);
        Task IncreaseLikesByStatisticId(int statisticId);
    }
}