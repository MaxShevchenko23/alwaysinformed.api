using alwaysinformed.Validation;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace alwaysinformed.Controllers
{
    //[Authorize]
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService service;

        public CategoryController(CategoryService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryGetDto>>> GetAllCategoriesAsync()
        {
            var dtos = await service.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("id")]
        public async Task<ActionResult<CategoryGetDto>> GetCategoryById([FromQuery] int id)
        {
            var article = await service.GetByIdAsync(id) ?? throw new APIException("ArgumentCannotBeNull");
            return Ok(article);
        }
        //all func-s below are accessable only for admins
        //[HttpPost]
        //public async Task<ActionResult<IEnumerable<ArticleGetShortDto>>> CategoryPostAsync([FromBody] CategoryPost dto)
        //{
        //    await service.AddAsync(dto);
        //    return Ok();
        //}

        //[HttpDelete]
        //public async Task<ActionResult> CategoryDeletebyIdAsync([FromQuery] int id)
        //{
        //    await service.DeleteByIdAsync(id);
        //    return Ok();
        //}
        //[HttpPut]
        //public async Task<ActionResult> GetShortLastArticlesAsync([FromBody] CategoryUpdateDto dto)
        //{
        //    await service.UpdateAsync(dto);
        //    return Ok();
        //}
    }
}
