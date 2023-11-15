using alwaysinformed_bll.AdditionalServices;
using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_bll.Validation;
using alwaysinformed_dal.Data;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

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
        //Author
        public async Task AddAsync(ArticleSandboxPostDto model)
        {
            var entity = mapper.Map<ArticleSandbox>(model);
            entity.Url = UrlGenerator.GenerateUrl();
            entity.PublicationDate = DateTime.Now;
            entity.ArticleStatus = 5;//Pending Approval
            await unitOfWork.ArticleSandboxRepository.AddAsync(entity);
            await unitOfWork.SaveChanges();
        }
        //Author
        public async Task AddAsADraftAsync(ArticleSandboxPostDto model)
        {
            var entity = mapper.Map<ArticleSandbox>(model);
            entity.Url = UrlGenerator.GenerateUrl();
            entity.PublicationDate = DateTime.Now;
            entity.ArticleStatus = 1; //Draft
            await unitOfWork.ArticleSandboxRepository.AddAsync(entity);
            await unitOfWork.SaveChanges();
        }
        //Admin
        public async Task DeleteByIdAsync(int modelId)
        {
            await unitOfWork.ArticleSandboxRepository.DeleteByIdAsync(modelId);
            await unitOfWork.SaveChanges();
        }
        //Author
        public async Task ArchiveByIdAsync(int modelId)
        {
            var entity = await unitOfWork.ArticleSandboxRepository.GetByIdAsync(modelId);
            entity.ArticleStatus = 3;//Archived
            unitOfWork.ArticleSandboxRepository.Update(entity);
            await unitOfWork.SaveChanges();
        }
        //Admin
        public async Task<IEnumerable<ArticleSandboxGetDto>> GetAllAsync()
        {
            var entities = await unitOfWork.ArticleSandboxRepository.GetAllAsync();
            return entities.Select(m => mapper.Map<ArticleSandboxGetDto>(m));
        }
        //Admin
        public async Task<ArticleSandboxGetDto> GetByIdAsync(int id)
        {
            var author = await unitOfWork.ArticleSandboxRepository.GetByIdAsync(id);
            return mapper.Map<ArticleSandboxGetDto>(author);
        }
        //Author
        public async Task UpdateAsync(ArticleSandboxUpdateDto model)
        {
            var entity = mapper.Map<ArticleSandbox>(model);
            entity.ArticleStatus = 5;//Pending approval
            unitOfWork.ArticleSandboxRepository.Update(entity);
            await unitOfWork.SaveChanges();
        }
        //Admin
        public async Task PublishAsync(int articleSandboxId)
        {
            try
            {
                ArticleService articleService = new(unitOfWork, mapper);

                var entity = await unitOfWork.ArticleSandboxRepository.GetByIdAsync(articleSandboxId) ?? throw new WebException();
                entity.ArticleStatus = 2;//Published
                unitOfWork.ArticleSandboxRepository.Update(entity);

                var article = await unitOfWork.ArticleRepository.GetArticleByArticleSandboxId(entity.SandboxId);

                if (article != null)
                {
                    var articleUpdate = mapper.Map<ArticleUpdateDto>(entity);
                    articleUpdate.ArticleId = article.ArticleId;
                    articleUpdate.ArticleSandboxId = entity.SandboxId;
                    //--->
                    await articleService.UpdateAsync(articleUpdate);
                    //<---
                }
                else
                {
                    var articlePost = mapper.Map<ArticlePostDto>(entity);
                    articlePost.ArticleSandboxId = entity.SandboxId;
                    await articleService.AddAsync(articlePost);
                }

                await unitOfWork.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                Log.Warning("InvalidOperationException occured, but data");
            }
        }
        //Author
        public async Task<IEnumerable<ArticleSandboxGetDto>> GetByAuthorIdAsync(int authorId)
        {
            var articles = await unitOfWork.ArticleSandboxRepository.GetByAuthorId(authorId);
            return articles.Select(a => mapper.Map<ArticleSandboxGetDto>(a));
        }
        //Author
        public async Task<IEnumerable<ArticleSandboxGetDto>> GetByUserIdAsync(int userId)
        {
            var articles = await unitOfWork.ArticleSandboxRepository.GetByUserId(userId);
            return articles.Select(a => mapper.Map<ArticleSandboxGetDto>(a));
        }
    }
}
