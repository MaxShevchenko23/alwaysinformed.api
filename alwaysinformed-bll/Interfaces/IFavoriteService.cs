using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
namespace alwaysinformed_bll.Interfaces
{
    public interface IFavoriteService:ICrud<FavoriteGetDto,FavoritePostDto,FavoriteUpdateDto>
    {
        Task<IEnumerable<ArticleGetShortDto>?> GetByUserId(int userId);

    }
}
