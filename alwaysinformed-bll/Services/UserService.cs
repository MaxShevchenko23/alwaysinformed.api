using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_dal.Data;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace alwaysinformed_bll.Services
{
    public class UserService:IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<UserGetDto> AddAsync(UserPostDto model)
        {
            var entity = mapper.Map<User>(model);
            var added = await unitOfWork.UserRepository.AddAsync(entity);
            return mapper.Map<UserGetDto>(added);
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await unitOfWork.UserRepository.DeleteByIdAsync(modelId);
            unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<UserGetDto>> GetAllAsync()
        {
            var entities = await unitOfWork.UserRepository.GetAllAsync();
            return entities.Select(c => mapper.Map<UserGetDto>(c));
        }

        public async Task<UserGetDto> GetByIdAsync(int id)
        {
            var entity = await unitOfWork.UserRepository.GetByIdAsync(id);
            return mapper.Map<UserGetDto>(entity);
        }

        public async Task<UserGetDto> GetByUsernameAndPasswordAsync(string username, string password)
        {
            var entity = await unitOfWork.UserRepository.GetByNameAndPassword(username, password);
            if (entity == null)
                return null;
            else return mapper.Map<UserGetDto>(entity);
        }

        public async Task<UserGetDto?> UpdateAsync(UserUpdateDto model)
        {
            User? source = new User();
            using (AidbContext context = new())
            {
                source = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == model.UserId) ?? null;
            }
            if(source == null)
            {
                return null;
            }
            var temp = new UserUpdateDto()
            {
                UserId = model.UserId,
                Username = model.Username != "" && model.Username.Length >= 8 ? model.Username : source.Username,
                UserPhoto = model.UserPhoto != "" && model.UserPhoto.Contains("https://") && model.UserPhoto.Length >= 15 ? model.UserPhoto : source.UserPhoto,
                PasswordHash = model.PasswordHash != "" && model.PasswordHash.Length >= 8 ? model.PasswordHash : source.PasswordHash,
                //we should send a letter via email to confirm changes
                Email = model != null && model.Email.Contains('@') && model.Email.Contains('.') && model.Email.Length > 5 ? model.Email : source.Email,
                UserRole = model.UserRole
            };

            var updated = await unitOfWork.UserRepository.Update(mapper.Map<User>(temp));
            return mapper.Map<UserGetDto>(updated);   
        }
    }
}
