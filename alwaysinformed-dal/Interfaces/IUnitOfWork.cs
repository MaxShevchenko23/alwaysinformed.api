using System.Security;

namespace alwaysinformed_dal.Interfaces
{
    public interface IUnitOfWork
    {
        public IArticleRepository ArticleRepository { get; }
        public IArticleSandboxRepository ArticleSandboxRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IFavoriteRepository FavoriteRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public IArticleSandboxStatusRepository ArticleSandboxStatusRepository { get; }


        public Task SaveChanges();

    }
}