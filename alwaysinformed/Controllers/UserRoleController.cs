using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace alwaysinformed.Controllers
{
    //[Authorize]
    [Route("api/userroles")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserRoleService service;

        public UserRoleController(UserRoleService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoleGetDto>>> GetAllStatusesAsync()
        {
            var userRoles = await service.UserRoleGetDtoAllAsync();
            return Ok(userRoles);
        }

        [HttpGet("id")]
        public async Task<ActionResult<UserRoleGetDto>> GetStatusByIdAsync([FromQuery] int id)
        {
            var userRoles = await service.UserRoleGetDtoByIdAsync(id);
            return Ok(userRoles);
        }

    }
}
