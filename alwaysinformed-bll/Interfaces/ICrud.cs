using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Interfaces
{
    public interface ICrud<Get,Post,Update>
    {
        Task<IEnumerable<Get>> GetAllAsync();

        Task<Get> GetByIdAsync(int id);

        Task AddAsync(Post model);

        Task UpdateAsync(Update model);

        Task DeleteByIdAsync(int modelId);
    }
}
