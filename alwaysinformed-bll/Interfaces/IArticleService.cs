using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;

namespace alwaysinformed_bll.Interfaces
{
    public interface IArticleService:ICrud<ArticleGetFullDto,ArticlePostDto,ArticleUpdateDto>
    {
        public Task<IEnumerable<ArticleGetShortDto>> GetAllShortAsync();
        public Task<ArticleGetShortDto> GetShortByIdAsync(int id);
        public Task<IEnumerable<ArticleGetShortDto>> GetShortLastNRecords(int n); 
        public Task<IEnumerable<ArticleGetFullDto>> GetFullLastNRecords(int n); 
        public Task<IEnumerable<ArticleGetFullDto>> GetFullFirstNRecords(int n); 
        public Task<IEnumerable<ArticleGetShortDto>> GetShortFirstNRecords(int n);
        public Task<ArticleGetFullDto> GetArticleByURL(string URL);

    }
}
