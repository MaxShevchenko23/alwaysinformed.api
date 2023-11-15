using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_dal.Interfaces;
using AutoMapper;

namespace alwaysinformed_bll.Services
{
    public class ArticleSandboxStatusService : IArticleSandboxStatusService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ArticleSandboxStatusService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<ArticleSandboxStatusGetDto>> ArticleSandboxStatusGetDtoAllAsync()
        {
            var statuses = await unitOfWork.ArticleSandboxStatusRepository.GetAllAsync();
            return statuses.Select(c => mapper.Map<ArticleSandboxStatusGetDto>(c)).ToList();
        }

        public async Task<ArticleSandboxStatusGetDto> ArticleSandboxStatusGetDtoByIdAsync(int id)
        {
            var statuses = await unitOfWork.ArticleSandboxStatusRepository.GetAllAsync();
            return mapper.Map<ArticleSandboxStatusGetDto>(statuses);
        }
    }
}
