using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using AutoMapper;

namespace alwaysinformed_bll.Services
{
    public class ArticleStatisticService : IArticleStatisticService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ArticleStatisticService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ArticleStatisticGetDto> AddAsync(ArticleStatisticPostDto model)
        {
            var entity = mapper.Map<ArticleStatistic>(model);
            var added = await unitOfWork.ArticleStatisticRepository.AddAsync(entity);

            return mapper.Map<ArticleStatisticGetDto>(added);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await unitOfWork.ArticleStatisticRepository.DeleteByIdAsync(modelId);
        }

        public async Task<IEnumerable<ArticleStatisticGetDto>> GetAllAsync()
        {
            var entities = await unitOfWork.ArticleStatisticRepository.GetAllAsync();

            return entities.Select(a=>mapper.Map<ArticleStatisticGetDto>(a));
        }

        public async Task<ArticleStatisticGetDto> GetByIdAsync(int id)
        {
            var entity = await unitOfWork.ArticleStatisticRepository.GetByIdAsync(id);
            return mapper.Map<ArticleStatisticGetDto>(entity);
        }

        public async Task<List<ArticleStatisticGetDto>> GetByUserIdAsync(int userId)
        {
            var entities = await unitOfWork.ArticleStatisticRepository.GetByUserIdAsync(userId);

            //var mapped = entities.Select(e => mapper.Map<ArticleStatisticGetDto>(e));
            //var modded = await Task.WhenAll(mapped.Select(async e =>
            //{
            //    var article = await unitOfWork.ArticleRepository.GetByIdAsync(e.ArticleId);
            //    e.ArticleName = article.Title;
            //    return e;
            //}));
            return entities.Select(e=>mapper.Map<ArticleStatisticGetDto>(e)).ToList();           
        }

        public async Task IncreaseLikesByArticleId(int articleId)
        {
           await unitOfWork.ArticleStatisticRepository.IncreaseLikesByArticleId(articleId);
        }

        public async Task IncreaseLikesByStatId(int statId)
        {
            await unitOfWork.ArticleStatisticRepository.IncreaseLikesByStatisticId(statId);
        }
        public async Task IncreaseViewsByArticleId(int articleId)
        {
            await unitOfWork.ArticleStatisticRepository.IncreaseViewsByArticleId(articleId);
        }
        public async Task IncreaseViewsByStatId(int statId)
        {
            await unitOfWork.ArticleStatisticRepository.IncreaseViewsByStatisticId(statId);
        }

        public Task<ArticleStatisticGetDto> UpdateAsync(ArticleStatisticUpdateDto model)
        {
            throw new NotImplementedException();
        }
    }
}
