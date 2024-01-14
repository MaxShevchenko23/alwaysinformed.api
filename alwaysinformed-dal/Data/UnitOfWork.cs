using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using alwaysinformed_dal.Repositories;

namespace alwaysinformed_dal.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AidbContext context;

        public UnitOfWork()
        {
            context = new AidbContext();
        }
        public IArticleRepository ArticleRepository => new ArticleRepository(context);

        public IArticleSandboxRepository ArticleSandboxRepository => new ArticleSandboxRepository(context);

        public IAuthorRepository AuthorRepository => new AuthorRepository(context);

        public ICommentRepository CommentRepository => new CommentRepository(context);

        public ICategoryRepository CategoryRepository => new CategoryRepository(context);

        public IFavoriteRepository FavoriteRepository => new FavoriteRepository(context);

        public IUserRepository UserRepository => new UserRepository(context);

        public IUserRoleRepository UserRoleRepository => new UserRoleRepository(context);

        public IArticleSandboxStatusRepository ArticleSandboxStatusRepository => new ArticleSandboxStatusRepository(context);

        public IArticleStatisticRepository ArticleStatisticRepository => new ArticleStatisticRepository(context);       

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
