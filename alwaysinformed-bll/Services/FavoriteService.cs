using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using AutoMapper;


namespace alwaysinformed_bll.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FavoriteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<FavoriteGetDto> AddAsync(FavoritePostDto model)
        {
            var entity = mapper.Map<Favorite>(model);
            var added = await unitOfWork.FavoriteRepository.AddAsync(entity);
            return mapper.Map<FavoriteGetDto>(added);
            
        }
        public async Task DeleteByIdAsync(int modelId)
        {
            await unitOfWork.FavoriteRepository.DeleteByIdAsync(modelId);
            unitOfWork.SaveChanges();
        }
        public async Task<IEnumerable<FavoriteGetDto>> GetAllAsync()
        {
            var favoriteEntities = await unitOfWork.FavoriteRepository.GetAllAsync();
            return favoriteEntities.Select(d => mapper.Map<FavoriteGetDto>(d));
        }
        public async Task<FavoriteGetDto> GetByIdAsync(int id)
        {
            var favorite = await unitOfWork.FavoriteRepository.GetByIdAsync(id);
            return mapper.Map<FavoriteGetDto>(favorite);
        }

        public async Task<IEnumerable<ArticleGetShortDto>?> GetByUserId(int userId)
        {
            var favorites = await unitOfWork.FavoriteRepository.GetByUserId(userId);
            return favorites.Select(a => mapper.Map<ArticleGetShortDto>(a));
        }

        public async Task<FavoriteGetDto> UpdateAsync(FavoriteUpdateDto model)
        {
            var entity = mapper.Map<Favorite>(model);
            var updated = unitOfWork.FavoriteRepository.Update(entity);
            return mapper.Map<FavoriteGetDto>(updated);
        }
    }
}
