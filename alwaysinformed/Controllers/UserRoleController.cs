using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Services;
using Microsoft.AspNetCore.Mvc;


namespace alwaysinformed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserRoleService service;

        public UserRoleController(UserRoleService service)
        {
            this.service = service;
        }
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<UserRoleGetDto>>> GetAllStatusesAsync()
        {
            var userRoles = await service.UserRoleGetDtoAllAsync();
            return Ok(userRoles);
        }

        [HttpGet("get/id")]
        public async Task<ActionResult<UserRoleGetDto>> GetStatusByIdAsync([FromQuery] int id)
        {
            var userRoles = await service.UserRoleGetDtoByIdAsync(id);
            return Ok(userRoles);
        }

    }
}
