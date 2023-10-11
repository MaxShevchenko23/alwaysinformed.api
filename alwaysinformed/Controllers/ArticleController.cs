using alwaysinformed.Entities;
using alwaysinformed.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using alwaysinformed.Services;

namespace alwaysinformed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly AidbContext context;
        private readonly IMapper mapper;

        public ArticleController(AidbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        //[HttpGet("/latestarticles/{n}")]
        //public ActionResult<IEnumerable<Article>> GetLatestArticles(int n)
        //{
        //    var articles = from a in context.Articles 
        //                   orderby a.ArticleId descending
        //                   select a ;

        //    return Ok(articles.Take(n));
        //}


        //[HttpGet("{articleurl}")]
        //public ActionResult<Article> GetArticle(string articleurl)
        //{
        //    var article = context.Articles.FirstOrDefault(a => a.Url == articleurl);
        //    if (article != null)
        //    {
        //        return Ok(article);
        //    }
        //    return NotFound();
        //}
        [HttpPost]
        public ActionResult<Article> CreateArticle(ArticleForCreatingDto articleDto)
        {           
            if (articleDto == null)
            {
                return ValidationProblem();
            }

            var article = mapper.Map<Article>(articleDto);
            
            article.Url = UrlGenerator.GenerateUrl();
            article.PublicationDate = DateTime.Now;

            context.SaveChanges(true);
            context.Articles.Add(article);

            return Ok(article);
        }

    }
}
