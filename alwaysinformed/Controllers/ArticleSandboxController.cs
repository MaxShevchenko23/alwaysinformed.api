using alwaysinformed.Validation;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_bll.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace alwaysinformed.Controllers
{
    [Route("[controller]")]
    [EnableCors("AllowLocalhost")]
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

        [HttpGet("id")]
        public async Task<ActionResult<ArticleSandboxGetDto>> GetArticleSandboxById([FromQuery] int id)
        {
            var article = await service.GetByIdAsync(id) ?? throw new APIException("ArgumentCannotBeNull");
            return Ok(article);
        }
        [HttpGet("url")]
        public async Task<ActionResult<ArticleSandboxGetDto>> GetArticleSandboxByURL([FromQuery] string url)
        {
            var article = await service.GetByURLAsync(url) ?? throw new APIException("ArgumentCannotBeNull");
            return Ok(article);
        }

        [HttpGet("userId")]
        public async Task<ActionResult<IEnumerable<ArticleSandboxGetDto>>> GetByUserIdAsync([FromQuery]int userId)
        {
            var dtos = await service.GetByUserIdAsync(userId);
            return Ok(dtos);
        }
        [HttpGet("authorId")]
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
        public async Task<ActionResult> UpdateArticleSandboxAsync([FromBody] ArticleSandboxUpdateDto dto)
        {
            await service.UpdateAsync(dto);
            return Ok();
        }
        //Admin
        [HttpPost("publish")]
        public async Task<ActionResult> PublishArticleFromSandbox([FromQuery] int articleSandboxId)
        {
            await service.PublishAsync(articleSandboxId);
            return Ok();
        }
        
        [HttpPut("decline")]
        public async Task<ActionResult> DeclineAsync([FromQuery] int id, [FromQuery] string comment)
        {
            await service.DeclineAsync(id,comment);
            return Ok();
        }
        [HttpPut("archive")]
        public async Task<ActionResult> ArchiveAsync([FromQuery] int id)
        {
            await service.ArchiveByIdAsync(id);
            return Ok();
        }
    }
}
