using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.UPDATE;
using alwaysinformed_bll.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Permissions;
using System.Security.Policy;

namespace alwaysinformed.Controllers
{
    //[Authorize]
    [EnableCors(PolicyName = "AllowLocalhost")]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ArticleService articleService;
        private readonly FavoriteService favoriteService;
        private readonly CommentService commentService;
        private readonly UserService userService;
        private readonly AuthorService authorService;
        private readonly int maxPageSize = 20;
        public UserController(ArticleService articleService,
            FavoriteService favoriteService,
            CommentService commentService,
            UserService userService,
            AuthorService authorService)
        {
            this.articleService = articleService;
            this.favoriteService = favoriteService;
            this.commentService = commentService;
            this.userService = userService;
            this.authorService = authorService;
        }
        //register user
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserGetDto>> UserAddAsync([FromBody] UserPostDto model)
        {
            var added = await userService.AddAsync(model);
            return Ok(added);
        }
        //articles
        [AllowAnonymous]
        [HttpGet("articles/filter")]
        public async Task<ActionResult<IEnumerable<ArticleGetShortDto>>> GetFilteredArticles([FromQuery] string? categoryName,
        [FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? searchQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {

            if (pageSize > maxPageSize)
                pageSize = maxPageSize;

            var (articles, paginationMeta) = await articleService.GetFilteredArticles(categoryName, firstName, lastName, searchQuery, pageNumber, pageSize);

            HttpContext.Response.Headers.Add("page-current", $"{paginationMeta.currentPage}");
            HttpContext.Response.Headers.Add("page-size", $"{paginationMeta.pageSize}");
            HttpContext.Response.Headers.Add("page-total", $"{paginationMeta.totalPagesAmount}");
            HttpContext.Response.Headers.Add("items-count", $"{paginationMeta.totalItemCount}");

            return Ok(articles);
        }
        [AllowAnonymous]
        [HttpGet("articles/id")]
        public async Task<ActionResult<ArticleGetFullDto>> ArticleGetByIdAsync(int id)
        {
            var article = await articleService.GetByIdAsync(id);
            if (article == null)
                return NotFound();
            var authorCredentials = await authorService.GetByIdAsync(article.AuthorId);

            article.FirstName = authorCredentials.FirstName;
            article.LastName = authorCredentials.LastName;

            article.Comments = await commentService.GetByArticleIdAsync(article.ArticleId);

            return Ok(article);
        }
        [AllowAnonymous]
        [HttpGet("articles/url")]
        public async Task<ActionResult<ArticleGetFullDto>> GetFullArticleByURLAsync([FromQuery] string url)
        {
            var article = await articleService.GetArticleByURL(url);
            if (article == null)
                return NotFound();
            var authorCredentials = await authorService.GetByIdAsync(article.AuthorId);
            
            article.FirstName = authorCredentials.FirstName;
            article.LastName = authorCredentials.LastName;

            article.Comments = await commentService.GetByArticleIdAsync(article.ArticleId);

            return Ok(article);
        }

        //favorites
        [HttpGet("favs/userId")]
        public async Task<ActionResult<IEnumerable<FavoriteGetDto>>> GetFavsByUserId([FromQuery]int userId)
        {
            var favs = await favoriteService.GetByUserId(userId);
            if (favs == null)
            {
                return NotFound();
            }
            return Ok(favs);
        }

        [HttpPost("favs/add")]
        public async Task<ActionResult<IEnumerable<ArticleGetShortDto>>> FavoritePostAsync([FromBody] FavoritePostDto dto)
        {
            await favoriteService.AddAsync(dto);
            return NoContent();
        }
        [HttpDelete("favs/remove")]
        public async Task<ActionResult> FavoriteDeletebyIdAsync([FromQuery] int id)
        {
            await favoriteService.DeleteByIdAsync(id);
            return NoContent();
        }
        //comments

        //get comments by articleId/articleUrl (сделать)
        

        [HttpPost("comms/add")]
        public async Task<ActionResult<CommentGetDto>> CommentPostAsync([FromBody] CommentPostDto dto)
        {
            var posted = await commentService.AddAsync(dto);
            return Ok(posted);
        }
        [HttpDelete("comms/remove")]
        public async Task<ActionResult> CommentDeletebyIdAsync([FromQuery] int id)
        {
            await commentService.DeleteByIdAsync(id);
            return NoContent();
        }
        [HttpPut("comms/edit")]
        public async Task<ActionResult> CommentUpdateAsync([FromBody] CommentUpdateDto dto)
        {
            var updated = await commentService.UpdateAsync(dto);
            return Ok(updated);
        }
        //edit user data
        [HttpPut("edit")]
        public async Task<ActionResult> UpdateUserAsync([FromBody] UserUpdateDto model)
        {
            model.Username = model.Username ?? string.Empty;
            model.UserPhoto = model.UserPhoto ?? string.Empty;
            model.PasswordHash = model.PasswordHash ?? string.Empty;
            model.Email = model.Email ?? string.Empty;
            var updated = await userService.UpdateAsync(model);

            if (updated == null) return NotFound();

            return Ok();
        }
        //bind author profile to user
        [HttpPost("bind/author")]
        public async Task<ActionResult<AuthorGetDto>> AuthorPostAsync([FromBody] AuthorPostDto model)
        {
            var created = await authorService.AddAsync(model);
            return Ok(created);
        }
    }
}
