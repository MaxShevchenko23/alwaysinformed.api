using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using alwaysinformed_dal.Data;
using alwaysinformed_bll.Services;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;

namespace alwaysinformed.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService service;

        public UserController(UserService service)
        {
            this.service = service;
        }
         
        [HttpGet("id")]
        public async Task<ActionResult> UserGetAsync([FromQuery] int id)
        {
            var user = await service.GetByIdAsync(id) ?? throw new ArgumentNullException();
            return Ok(user);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGetDto>>> GetAllUsersAsync()
        {
            var dtos = await service.GetAllAsync();
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<UserGetDto>> UserAddAsync([FromBody] UserPostDto model)
        {
            var added = await service.AddAsync(model);
            return Ok(added);
        }
       
        [HttpDelete]
        public async Task<ActionResult> UserDeleteAsync([FromQuery] int id)
        {
            await service.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<UserGetDto>> UpdateUserAsync([FromBody] UserUpdateDto model)
        {
            var updated = await service.UpdateAsync(model);
            return Ok(updated);
        }
    }
}
