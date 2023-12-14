using alwaysinformed.Validation;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_bll.Services;
using Microsoft.AspNetCore.Mvc;


namespace alwaysinformed.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentService service;

        public CommentController(CommentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentGetDto>>> GetAllCommentsAsync()
        {
            var dtos = await service.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("id")]
        public async Task<ActionResult<CommentGetDto>> GetCommentById([FromQuery] int id)
        {
            var article = await service.GetByIdAsync(id) ?? throw new APIException("ArgumentCannotBeNull");
            return Ok(article);
        }

        [HttpGet("articleId")]
        public async Task<ActionResult<IEnumerable<CommentGetDto>>> GetCommentsByArticleId([FromQuery] int articleId)
        {
            var entity = await service.GetByArticleIdAsync(articleId);
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult> CommentPostAsync([FromBody] CommentPostDto dto)
        {
            await service.AddAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> CommentDeletebyIdAsync([FromQuery] int id)
        {
            await service.DeleteByIdAsync(id);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> CommentUpdateAsync([FromBody] CommentUpdateDto dto)
        {
            await service.UpdateAsync(dto);
            return Ok();
        }
    }
}
