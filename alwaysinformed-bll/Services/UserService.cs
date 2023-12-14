﻿using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_dal.Entities;
using alwaysinformed_dal.Interfaces;
using AutoMapper;

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

        public async Task<UserGetDto> UpdateAsync(UserUpdateDto model)
        {
            var updated = await unitOfWork.UserRepository.Update(mapper.Map<User>(model));
            return mapper.Map<UserGetDto>(updated);
            
        }
    }
}
