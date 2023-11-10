using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using alwaysinformed_dal.Data;
using alwaysinformed_bll.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Humanizer;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;

namespace alwaysinformed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService service;

        public UserController(UserService service)
        {
            this.service = service;
        }
         
        [HttpGet("get")]
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
        public async Task<ActionResult> UserPostAsync([FromBody] UserPostDto model)
        {
            await service.AddAsync(model);
            return Ok();
        }
       
        [HttpDelete]
        public async Task<ActionResult> UserDeleteAsync([FromQuery] int id)
        {
            await service.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserAsync([FromBody] UserUpdateDto model)
        {
            await service.UpdateAsync(model);
            return Ok();
        }
    }
}
