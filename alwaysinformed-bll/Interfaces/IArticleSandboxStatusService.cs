using alwaysinformed_bll.Models.GET;


namespace alwaysinformed_bll.Interfaces
{
    public interface IArticleSandboxStatusService
    {
        Task<IEnumerable<ArticleSandboxStatusGetDto>> ArticleSandboxStatusGetDtoAllAsync();

        Task<ArticleSandboxStatusGetDto> ArticleSandboxStatusGetDtoByIdAsync(int id);
    }
}
