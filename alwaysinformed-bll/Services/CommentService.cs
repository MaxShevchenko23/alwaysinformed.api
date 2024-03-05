using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using AutoMapper;


namespace alwaysinformed_bll.Services
{
    public class CommentService:ICommentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<CommentGetDto> AddAsync(CommentPostDto model)
        {
            var entity = mapper.Map<Comment>(model);
            entity.DateTime = DateTime.UtcNow;
            
            var added = await unitOfWork.CommentRepository.AddAsync(entity);
            return mapper.Map<CommentGetDto>(added);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await unitOfWork.CommentRepository.DeleteByIdAsync(modelId);
            unitOfWork.SaveChanges();
        }
        public async Task<IEnumerable<CommentGetDto>> GetByArticleIdAsync(int id)
        {
            var entity = await unitOfWork.CommentRepository.GetCommentByArticleId(id);
            return entity.Select(m=>mapper.Map<CommentGetDto>(m));
        }

        public async Task<IEnumerable<CommentGetDto>> GetAllAsync()
        {
            var entities = await unitOfWork.CommentRepository.GetAllAsync();
            return entities.Select(c => mapper.Map<CommentGetDto>(c));
        }

        public async Task<CommentGetDto> GetByIdAsync(int id)
        {
            var entity = await unitOfWork.CommentRepository.GetByIdAsync(id);
            return mapper.Map<CommentGetDto>(entity);
        }

        public async Task<CommentGetDto> UpdateAsync(CommentUpdateDto model)
        {
            var updated = await unitOfWork.CommentRepository.Update(mapper.Map<Comment>(model));
            return mapper.Map<CommentGetDto>(updated);
        }
    }
}
