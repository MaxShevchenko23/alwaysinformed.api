using alwaysinformed_bll.AdditionalServices;
using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using AutoMapper;

namespace alwaysinformed_bll.Services
{
    public class ArticleSandboxService : IArticleSandboxService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ArticleSandboxService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task AddAsync(ArticlePostDto model)
        {
            var entity = mapper.Map<ArticleSandbox>(model);
            entity.Url = UrlGenerator.GenerateUrl();
            entity.PublicationDate = DateTime.Now;
            await unitOfWork.ArticleSandboxRepository.AddAsync(entity);
            unitOfWork.SaveChanges();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await unitOfWork.ArticleSandboxRepository.DeleteByIdAsync(modelId);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<ArticleGetFullDto>> GetAllAsync()
        {
            var entities = await unitOfWork.ArticleSandboxRepository.GetAllAsync();
            return entities.Select(m => mapper.Map<ArticleGetFullDto>(m));
        }

        public async Task<ArticleGetFullDto> GetByIdAsync(int id)
        {
            var author = await unitOfWork.ArticleSandboxRepository.GetByIdAsync(id);
            return mapper.Map<ArticleGetFullDto>(author);
        }

        public async Task UpdateAsync(ArticleUpdateDto model)
        {
            var entity = mapper.Map<ArticleSandbox>(model);
            unitOfWork.ArticleSandboxRepository.Update(entity);
            unitOfWork.SaveChanges();
        }
    }
}
