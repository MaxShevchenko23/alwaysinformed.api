using alwaysinformed_dal.Data;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_dal.Repositories
{
    public class ArticleSandboxStatusRepository:IArticleSandboxStatusRepository
    {
        private readonly AidbContext context;

        public ArticleSandboxStatusRepository(AidbContext context)
        {
            this.context = context;
        }

        public Task<ArticleSandboxStatus> AddAsync(ArticleSandboxStatus entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(ArticleSandboxStatus entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ArticleSandboxStatus>> GetAllAsync()
        {
            return await context.ArticleSandboxStatuses.ToListAsync();
        }

        public async Task<ArticleSandboxStatus> GetByIdAsync(int id)
        {
            return await context.ArticleSandboxStatuses.FirstOrDefaultAsync(c => c.StatusId == id);
        }

        public Task<ArticleSandboxStatus> Update(ArticleSandboxStatus entity)
        {
            throw new NotImplementedException();
        }
    }
}
