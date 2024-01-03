using alwaysinformed.Validation;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_bll.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace alwaysinformed.Controllers
{
    [EnableCors("AllowLocalhost")]
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ArticleSandboxService sandboxService;
        private readonly CategoryService categoryService;

        public AuthorController(ArticleSandboxService sandboxService,CategoryService categoryService)
        {
            this.sandboxService = sandboxService;
            this.categoryService = categoryService;
        }
        //sandbox
        [HttpGet("sandbox")]
        public async Task<ActionResult<ArticleSandboxGetDto>> AllArticleSandbox()
        {
            var article = await sandboxService.GetAllAsync() ?? throw new APIException("ArgumentCannotBeNull");
            return Ok(article);
        }

        [HttpGet("sandbox/id")]
        public async Task<ActionResult<ArticleSandboxGetDto>> GetArticleSandboxById([FromQuery] int id)
        {
            var article = await sandboxService.GetByIdAsync(id) ?? throw new APIException("ArgumentCannotBeNull");
            return Ok(article);
        }
        [HttpGet("sandbox/url")]
        public async Task<ActionResult<ArticleSandboxGetDto>> GetArticleSandboxByURL([FromQuery] string url)
        {
            var article = await sandboxService.GetByURLAsync(url);
            if (article == null) return NotFound();
            return Ok(article);
        }
        [HttpGet("sandbox/userId")]
        public async Task<ActionResult<IEnumerable<ArticleSandboxGetDto>>> GetByUserIdAsync([FromQuery] int userId)
        {
            var dtos = await sandboxService.GetByUserIdAsync(userId);
            return Ok(dtos);
        }
        [HttpGet("sandbox/authorId")]
        public async Task<ActionResult<IEnumerable<ArticleSandboxGetDto>>> GetByAuthorIdAsync([FromQuery] int authorId)
        {
            var dtos = await sandboxService.GetByAuthorIdAsync(authorId);
            return Ok(dtos);
        }
        [HttpPost("sandbox/add")]
        public async Task<ActionResult> ArticleSandboxPostAsync([FromBody] ArticleSandboxPostDto dto)
        {
            var articles = await sandboxService.AddAsync(dto);
            return Ok(articles);
        }
        //+-
        [HttpDelete("sandbox/remove")]
        public async Task<ActionResult> ArticleSandboxDeletebyIdAsync([FromQuery] int id)
        {
            await sandboxService.DeleteByIdAsync(id);
            return Ok();
        }
        [HttpPut("sandbox/update")]
        public async Task<ActionResult> UpdateArticleSandboxAsync([FromBody] ArticleSandboxUpdateDto dto)
        {
            await sandboxService.UpdateAsync(dto);
            return Ok();
        }
        //++
        [HttpPut("sandbox/archive")]
        public async Task<ActionResult> ArchiveAsync([FromQuery] int id)
        {
            await sandboxService.ArchiveByIdAsync(id);
            return Ok();
        }
        //categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryGetDto>>> GetAllCategoriesAsync()
        {
            var dtos = await categoryService.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("id")]
        public async Task<ActionResult<CategoryGetDto>> GetCategoryById([FromQuery] int id)
        {
            var article = await categoryService.GetByIdAsync(id) ?? throw new APIException("ArgumentCannotBeNull");
            return Ok(article);
        }
    }
}
