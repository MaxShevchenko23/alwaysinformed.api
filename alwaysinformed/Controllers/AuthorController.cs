using alwaysinformed.Validation;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_bll.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alwaysinformed.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly AuthorService service;

    public AuthorController(AuthorService service)
    {
        this.service = service;
    }

    [HttpGet("id")]
    public async Task<ActionResult> AuthorGetByIdAsync([FromQuery] int id)
    {
        var author = await service.GetByIdAsync(id) ?? throw new APIException("ArgumentCannotBeNull");
        return Ok(author);
    }
    
    [HttpGet]
    public async Task<ActionResult> AuthorGetAllAsync()
    {
        var entities = await service.GetAllAsync();
        return Ok(entities);
    }
    
    [HttpPost]
    public async Task<ActionResult> AuthorPostAsync([FromBody] AuthorPostDto model)
    {
        await service.AddAsync(model);
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> AuthorUpdateAsync([FromBody] AuthorUpdateDto model)
    {
        await service.UpdateAsync(model);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> AuthorDeleteByIdAsync(int id)
    {
        await service.DeleteByIdAsync(id);
        return Ok();
    }
}
