using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;


namespace alwaysinformed_bll.Interfaces
{
    public interface IUserService:ICrud<UserGetDto,UserPostDto,UserUpdateDto>
    {
        Task<UserGetDto?> GetByUsernameAndPasswordAsync(string username, string password);
    }
}
