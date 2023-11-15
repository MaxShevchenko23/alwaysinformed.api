using alwaysinformed_bll.AdditionalServices;
using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Services
{
    public class ArticleService : IArticleService,IDisposable
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task AddAsync(ArticlePostDto model)
        {
            var entity = mapper.Map<Article>(model);
            entity.Url = UrlGenerator.GenerateUrl();
            entity.PublicationDate = DateTime.Now;
            await unitOfWork.ArticleRepository.AddAsync(entity);
            await unitOfWork.SaveChanges();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await unitOfWork.ArticleRepository.DeleteByIdAsync(modelId);
            await unitOfWork.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
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

        public async Task UpdateAsync(ArticleUpdateDto model)
        {
            var entity = mapper.Map<Article>(model);
            unitOfWork.ArticleRepository.Update(entity);
            await unitOfWork.SaveChanges();
        }
    }
}
