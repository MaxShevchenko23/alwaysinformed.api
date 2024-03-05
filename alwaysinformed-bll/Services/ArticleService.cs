using alwaysinformed_bll.AdditionalServices;
using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace alwaysinformed_bll.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ArticleGetFullDto> AddAsync(ArticlePostDto model)
        {
            var entity = mapper.Map<Article>(model);
            entity.Url = UrlGenerator.GenerateUrl();
            entity.PublicationDate = DateTime.Now;
            var createdArticle =  await unitOfWork.ArticleRepository.AddAsync(entity);

            var createdStats = await unitOfWork.ArticleStatisticRepository.AddAsync(new() { ArticleId = createdArticle.ArticleId });

            await unitOfWork.SaveChanges();
            return mapper.Map<ArticleGetFullDto>(createdArticle);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await unitOfWork.ArticleRepository.DeleteByIdAsync(modelId);
            await unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<ArticleGetFullDto>> GetAllAsync()
        {
            var entities = await unitOfWork.ArticleRepository.GetAllAsync();
            return entities.Select(m => mapper.Map<ArticleGetFullDto>(m));
        }
        public async Task<IEnumerable<ArticleGetShortDto>> GetAllShortAsync()
        {
            var articleEntities = await unitOfWork.ArticleRepository.GetAllAsync();
            return articleEntities.Select(d => mapper.Map<ArticleGetShortDto>(d));
        }

        public async Task<ArticleGetFullDto> GetArticleByURL(string URL)
        {
            var article = await unitOfWork.ArticleRepository.GetArticleByURL(URL);
            return mapper.Map<ArticleGetFullDto>(article);
        }

        public async Task<ArticleGetFullDto> GetByIdAsync(int id)
        {
            var author = await unitOfWork.ArticleRepository.GetByIdAsync(id);
            return mapper.Map<ArticleGetFullDto>(author);
        }

        public async Task<IEnumerable<ArticleGetFullDto>> GetFullFirstNRecords(int n)
        {
            var articles = await unitOfWork.ArticleRepository.GetFirstRecords(n);
            return articles.Select(m => mapper.Map<ArticleGetFullDto>(m));
        }

        public async Task<IEnumerable<ArticleGetFullDto>> GetFullLastNRecords(int n)
        {
            var articles = await unitOfWork.ArticleRepository.GetLastRecords(n);
            return articles.Select(m => mapper.Map<ArticleGetFullDto>(m));
        }

        public async Task<ArticleGetShortDto> GetShortByIdAsync(int id)
        {
            var articleShort = await unitOfWork.ArticleRepository.GetByIdAsync(id);
            return mapper.Map<ArticleGetShortDto>(articleShort);
        }

        public async Task<IEnumerable<ArticleGetShortDto>> GetShortFirstNRecords(int n)
        {
            var articles = await unitOfWork.ArticleRepository.GetFirstRecords(n);
            return articles.Select(m => mapper.Map<ArticleGetShortDto>(m));
        }

        public async Task<IEnumerable<ArticleGetShortDto>> GetShortLastNRecords(int n)
        {
            var articles = await unitOfWork.ArticleRepository.GetLastRecords(n);
            return articles.Select(m => mapper.Map<ArticleGetShortDto>(m));
        }

        public async Task<ArticleGetFullDto> UpdateAsync(ArticleUpdateDto model)
        {
            var entity = mapper.Map<Article>(model);
            var updated = await unitOfWork.ArticleRepository.Update(entity);
            return mapper.Map<ArticleGetFullDto>(updated);
        }

        public async Task<(IEnumerable<ArticleGetShortDto>, PagingMetaInfo)> GetFilteredArticles(string? categoryName, string? firstName, string? lastName, string? searchQuery, int pageNumber, int pageSize)
        {
            var articles = await unitOfWork.ArticleRepository.GetAllQueryableAsync();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                categoryName = categoryName.Trim();
                articles = articles.Where(a => a.Category.CategoryName.Contains(categoryName));
            }

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                firstName = firstName.Trim();
                articles = articles.Where(a => a.Author.FirstName.ToLower().Contains(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                lastName = lastName.Trim();
                articles = articles.Where(a => a.Author.LastName.ToLower().Contains(categoryName));
            }
            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                articles = articles.Where(a=>a.Content.Contains(searchQuery) || a.Title.Contains(searchQuery) || a.ShortDescription.Contains(searchQuery));
            }

            articles = articles.OrderByDescending(a => a.ArticleId)
                               .Skip(pageSize * (pageNumber - 1))
                               .Take(pageSize);

            int totalCount = await articles.CountAsync();

            var articlesToReturn = articles.Select(a => mapper.Map<ArticleGetShortDto>(a));
            var paginationMeta = new PagingMetaInfo(totalCount, pageSize, pageNumber);
            
            return (articlesToReturn, paginationMeta);
        }
    }
}
