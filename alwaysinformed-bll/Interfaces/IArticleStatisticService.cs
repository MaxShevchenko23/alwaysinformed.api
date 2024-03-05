using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_dal.Entities;

namespace alwaysinformed_bll.Interfaces
{
    public interface IArticleStatisticService : ICrud<ArticleStatisticGetDto, ArticleStatisticPostDto, ArticleStatisticUpdateDto>
    {
        Task IncreaseLikesByArticleId(int articleId);
        Task IncreaseLikesByStatId(int statId);
        Task IncreaseViewsByArticleId(int articleId);
        Task IncreaseViewsByStatId(int statId);
        Task<List<ArticleStatisticGetDto>> GetByUserIdAsync(int userId);
    }
}