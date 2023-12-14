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
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AidbContext context;

        public UserRoleRepository(AidbContext context)
        {
            this.context = context;
        }
        public Task<UserRole> AddAsync(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserRole>> GetAllAsync()
        {
            return await context.UserRoles.ToListAsync();
        }

        public async Task<UserRole> GetByIdAsync(int id)
        {
            return await context.UserRoles.FirstOrDefaultAsync(c => c.UserRoleId == id);
        }

        public Task<UserRole> Update(UserRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
