using alwaysinformed_dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_dal.Interfaces
{
    public interface IArticleRepository:IRepository<Article>
    {
        Task AddFromSandbox(Article article);
        Task<List<Article>> GetFirstRecords(int amount);

        Task<List<Article>> GetLastRecords(int amount);
        Task<Article> GetArticleByURL(string url);
    }
}
