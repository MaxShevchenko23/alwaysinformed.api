using alwaysinformed_dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_dal.Interfaces
{
    public interface IArticleSandboxRepository:IRepository<ArticleSandbox>
    {
        Task<List<ArticleSandbox>> GetByAuthorId(int id);
        Task<List<ArticleSandbox>> GetByUserId(int id);
        Task<ArticleSandbox> GetByURLAsync(string url);

    }
}
