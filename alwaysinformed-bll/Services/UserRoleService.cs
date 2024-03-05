using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_dal.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alwaysinformed_bll.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserRoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<UserRoleGetDto>> UserRoleGetDtoAllAsync()
        {
            var userRoles = await unitOfWork.UserRoleRepository.GetAllAsync();
            return userRoles.Select(c =>mapper.Map<UserRoleGetDto>(c));
        }

        public async Task<UserRoleGetDto> UserRoleGetDtoByIdAsync(int id)
        {
            var userRole = await unitOfWork.UserRoleRepository.GetAllAsync();
            return mapper.Map<UserRoleGetDto>(userRole);
        }
    }
}
