using alwaysinformed.Validation;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_bll.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alwaysinformed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleSandboxController : ControllerBase
    {
        private readonly ArticleSandboxService service;

        public ArticleSandboxController(ArticleSandboxService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleSandboxGetDto>>> GetAllArticlesSandboxAsync()
        {
            var dtos = await service.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("get")]
        public async Task<ActionResult<ArticleSandboxGetDto>> GetArticleSandboxById([FromQuery] int id)
        {
            var article = await service.GetByIdAsync(id) ?? throw new APIException("ArgumentCannotBeNull");
            return Ok(article);
        }

        [HttpGet("getbyuserid")]
        public async Task<ActionResult<IEnumerable<ArticleSandboxGetDto>>> GetByUserIdAsync([FromQuery]int userId)
        {
            var dtos = await service.GetByUserIdAsync(userId);
            return Ok(dtos);
        }
        [HttpGet("getbyauthorid")]
        public async Task<ActionResult<IEnumerable<ArticleSandboxGetDto>>> GetByAuthorIdAsync([FromQuery]int authorId)
        {
            var dtos = await service.GetByUserIdAsync(authorId);
            return Ok(dtos);
        }
        [HttpPost]
        public async Task<ActionResult> ArticleSandboxPostAsync([FromBody] ArticleSandboxPostDto dto)
        {
            await service.AddAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> ArticleSandboxDeletebyIdAsync([FromQuery] int id)
        {
            await service.DeleteByIdAsync(id);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> GetShortLastArticlesAsync([FromBody] ArticleSandboxUpdateDto dto)
        {
            await service.UpdateAsync(dto);
            return Ok();
        }
        //Admin
        [HttpPut("publish")]
        public async Task<ActionResult> PublishArticleFromSandbox([FromQuery] int articleSandboxId)
        {
            await service.PublishAsync(articleSandboxId);
            return Ok();
        }
    }
}
