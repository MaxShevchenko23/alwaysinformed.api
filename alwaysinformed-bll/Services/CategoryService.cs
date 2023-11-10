using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using AutoMapper;


namespace alwaysinformed_bll.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task AddAsync(CategoryPost model)
        {
            var entity = mapper.Map<Category>(model);
            await unitOfWork.CategoryRepository.AddAsync(entity);
            unitOfWork.SaveChanges();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await unitOfWork.CategoryRepository.DeleteByIdAsync(modelId);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<CategoryGetDto>> GetAllAsync()
        {
            var entities = await unitOfWork.CategoryRepository.GetAllAsync();
            return entities.Select(d =>mapper.Map<CategoryGetDto>(d));
        }

        public async Task<CategoryGetDto> GetByIdAsync(int id)
        {
            var entity = await unitOfWork.CategoryRepository.GetByIdAsync(id);
            return mapper.Map<CategoryGetDto>(entity);
        }

        public async Task UpdateAsync(CategoryUpdateDto model)
        {
            unitOfWork.CategoryRepository.Update(mapper.Map<Category>(model));
            unitOfWork.SaveChanges();
        }
    }
}
