using alwaysinformed_bll.Models.GET;

namespace alwaysinformed_bll.Interfaces
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleGetDto>> UserRoleGetDtoAllAsync();

        Task<UserRoleGetDto> UserRoleGetDtoByIdAsync(int id);
    }
}
