using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task AddAsync(AuthorPostDto model)
        {
            var entity = mapper.Map<Author>(model);
            await unitOfWork.AuthorRepository.AddAsync(entity);
            unitOfWork.SaveChanges();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await unitOfWork.AuthorRepository.DeleteByIdAsync(modelId);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<AuthorGetDto>> GetAllAsync()
        {
            var authorEntities = await unitOfWork.AuthorRepository.GetAllAsync();
            return authorEntities.Select(d => mapper.Map<AuthorGetDto>(d));
        }

        public async Task<AuthorGetDto> GetByIdAsync(int modelId)
        {
            var author = await unitOfWork.AuthorRepository.GetByIdAsync(modelId);
            return mapper.Map<AuthorGetDto>(author);
        }

        public async Task UpdateAsync(AuthorUpdateDto model)
        {
            var entity = mapper.Map<Author>(model);
            unitOfWork.AuthorRepository.Update(entity);
            unitOfWork.SaveChanges();
        }
    }
}
