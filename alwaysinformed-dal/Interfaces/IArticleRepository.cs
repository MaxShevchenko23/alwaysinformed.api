using alwaysinformed_dal.Entities;


namespace alwaysinformed_dal.Interfaces
{
    public interface IArticleRepository:IRepository<Article>,IFilter<Article>
    {
        Task AddFromSandbox(Article article);
        Task<List<Article>> GetFirstRecords(int amount);

        Task<List<Article>> GetLastRecords(int amount);
        Task<Article> GetArticleByURL(string url);
        Task<Article?> GetArticleByArticleSandboxId(int articleSandboxId);
        Task DeleteArticleBySandboxId(int sandboxId);
        Task<IQueryable<Article>> GetAllQueryableAsync();
    }
}
