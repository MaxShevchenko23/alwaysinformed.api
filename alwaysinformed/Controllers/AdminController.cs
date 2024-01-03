using alwaysinformed.Validation;
using alwaysinformed_bll.Interfaces;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_bll.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alwaysinformed.Controllers
{
    [Route("api/admin")]
    //[Authorize(Policy = "forAdmin")]
    //[EnableCors("AllowLocalhost")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ArticleSandboxService sandboxService;
        private readonly UserService userService;
        private readonly CommentService commentService;
        private readonly CategoryService categoryService;

        public AdminController(ArticleSandboxService sandboxService,
            UserService userService,
            CommentService commentService,
            CategoryService categoryService)
        {
            this.sandboxService = sandboxService;
            this.userService = userService;
            this.commentService = commentService;
            this.categoryService = categoryService;
        }
        //sandbox
        [HttpGet("sandbox")]
        public async Task<ActionResult<IEnumerable<ArticleSandboxGetDto>>> GetAllArticlesSandboxAsync()
        {
            var dtos = await sandboxService.GetAllAsync();
            return Ok(dtos);
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
            var article = await sandboxService.GetByURLAsync(url) ?? throw new APIException("ArgumentCannotBeNull");
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
            var dtos = await sandboxService.GetByUserIdAsync(authorId);
            return Ok(dtos);
        }
        [HttpPost("sandbox/publish")]
        public async Task<ActionResult> PublishArticleFromSandbox([FromQuery] int articleSandboxId)
        {
            await sandboxService.PublishAsync(articleSandboxId);
            return Ok();
        }
        [HttpPut("sandbox/decline")]
        public async Task<ActionResult> DeclineAsync([FromQuery] int id, [FromQuery] string comment)
        {
            await sandboxService.DeclineAsync(id, comment);
            return Ok();
        }
        [HttpPut("sandbox/archive")]
        public async Task<ActionResult> ArchiveAsync([FromQuery] int id)
        {
            await sandboxService.ArchiveByIdAsync(id);
            return Ok();
        }
        //users manipulations
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserGetDto>>> GetAllUsersAsync()
        {
            var dtos = await userService.GetAllAsync();
            return Ok(dtos);
        }
        [HttpGet("users/id")]
        public async Task<ActionResult> UserGetAsync([FromQuery] int id)
        {
            var user = await userService.GetByIdAsync(id) ?? throw new ArgumentNullException();
            return Ok(user);
        }
        [HttpDelete("users/remove")]
        public async Task<ActionResult> UserDeleteAsync([FromQuery] int id)
        {
            await userService.DeleteByIdAsync(id);
            return Ok();
        }
        [HttpPut("users/edit")]
        public async Task<ActionResult<UserGetDto>> UpdateUserAsync([FromBody] UserUpdateDto model)
        {
            var updated = await userService.UpdateAsync(model);
            return Ok(updated);
        }
        //comments
        [HttpGet("comms")]
        public async Task<ActionResult<IEnumerable<CommentGetDto>>> GetAllCommentsAsync()
        {
            var dtos = await commentService.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("comms/id")]
        public async Task<ActionResult<CommentGetDto>> GetCommentById([FromQuery] int id)
        {
            var article = await commentService.GetByIdAsync(id) ?? throw new APIException("ArgumentCannotBeNull");
            return Ok(article);
        }

        [HttpGet("comms/articleId")]
        public async Task<ActionResult<IEnumerable<CommentGetDto>>> GetCommentsByArticleId([FromQuery] int articleId)
        {
            var entity = await commentService.GetByArticleIdAsync(articleId);
            return Ok(entity);
        }

        [HttpDelete("comms/remove")]
        public async Task<ActionResult> CommentDeletebyIdAsync([FromQuery] int id)
        {
            await commentService.DeleteByIdAsync(id);
            return Ok();
        }
        //categories
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryGetDto>>> GetAllCategoriesAsync()
        {
            var dtos = await categoryService.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("categories/id")]
        public async Task<ActionResult<CategoryGetDto>> GetCategoryById([FromQuery] int id)
        {
            var article = await categoryService.GetByIdAsync(id) ?? throw new APIException("ArgumentCannotBeNull");
            return Ok(article);
        }

        [HttpPost("categories/add")]
        public async Task<ActionResult<IEnumerable<ArticleGetShortDto>>> CategoryPostAsync([FromBody] CategoryPost dto)
        {
            await categoryService.AddAsync(dto);
            return Ok();
        }

        [HttpDelete("categories/remove")]
        public async Task<ActionResult> CategoryDeletebyIdAsync([FromQuery] int id)
        {
            await categoryService.DeleteByIdAsync(id);
            return Ok();
        }
        [HttpPut("categories/edit")]
        public async Task<ActionResult> GetShortLastArticlesAsync([FromBody] CategoryUpdateDto dto)
        {
            await categoryService.UpdateAsync(dto);
            return Ok();
        }
    }
}
