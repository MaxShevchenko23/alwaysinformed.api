using alwaysinformed.Entities;
using alwaysinformed.Models;
using alwaysinformed.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace alwaysinformed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private AidbContext context;
        private IMapper mapper;

        public UserController(AidbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("/latestarticles/{n}")]
        public ActionResult<IEnumerable<Article>> GetLatestArticles(int n)
        {
            var articles = from a in context.Articles
                           orderby a.ArticleId descending
                           select a;

            return Ok(articles.Take(n));
        }

        [HttpGet("{articleurl}")]
        public ActionResult<Article> GetArticle(string articleurl)
        {
            var article = context.Articles.FirstOrDefault(a => a.Url == articleurl);
            if (article != null)
            {
                return Ok(article);
            }
            return NotFound();
        }
        [HttpPost("/addToFavorites/{userId}/{articleId}")]
        public ActionResult<Article> AddToFavorites(int userId, int articleId)
        {
            if (context.Users.FirstOrDefault(u => u.UserId == userId) == null || 
                context.Articles.FirstOrDefault(a => a.ArticleId == articleId) == null)
            {
                return NotFound();
            }
            context.Add(new Favorite { UserId = userId, ArticleId = articleId });
            context.SaveChanges(true);
            return Ok();
        }
    }
}
