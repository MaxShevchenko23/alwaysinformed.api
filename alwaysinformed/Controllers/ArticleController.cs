using alwaysinformed.Entities;
using alwaysinformed.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace alwaysinformed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly AidbContext context;

        public ArticleController(AidbContext context)
        {
            this.context = context;
        }

        [HttpGet("Home")]
        public ActionResult<IEnumerable<Article>> GetArticles()
        {
            List<ArticleHomeDto> list = new List<ArticleHomeDto>
            {
                new ArticleHomeDto
                {
                    Title = "Elo Mau",
                    PublicationDate = DateTime.Now,
                    Image =  "https://i.insider.com/64c7a2c9048ff200190deaf5?width=600&format=jpeg&auto=webp",
                    ShortDescription = "interesting article about Elo Mau",
                    URL = "templateURL"
                },
                new ArticleHomeDto
                {
                    Title = "Elo Mau1",
                    PublicationDate = DateTime.Now,
                    Image =  "https://i.insider.com/64c7a2c9048ff200190deaf5?width=600&format=jpeg&auto=webp",
                    ShortDescription = "interesting article about Elo Mau",
                    URL = "templateURL"
                },
                new ArticleHomeDto
                {
                    Title = "Elo Mau2",
                    PublicationDate = DateTime.Now,
                    Image =  "https://i.insider.com/64c7a2c9048ff200190deaf5?width=600&format=jpeg&auto=webp",
                    ShortDescription = "interesting article about Elo Mau",
                    URL = "templateURL"
                },
                new ArticleHomeDto
                {
                    Title = "Elo Mau3",
                    PublicationDate = DateTime.Now,
                    Image =  "https://i.insider.com/64c7a2c9048ff200190deaf5?width=600&format=jpeg&auto=webp",
                    ShortDescription = "interesting article about Elo Mau",
                    URL = "templateURL"
                },
                new ArticleHomeDto
                {
                    Title = "Elo Mau4",
                    PublicationDate = DateTime.Now,
                    Image =  "https://i.insider.com/64c7a2c9048ff200190deaf5?width=600&format=jpeg&auto=webp",
                    ShortDescription = "interesting article about Elo Mau",
                    URL = "templateURL"
                },
                new ArticleHomeDto
                {
                    Title = "Elo Mau5",
                    PublicationDate = DateTime.Now,
                    Image =  "https://i.insider.com/64c7a2c9048ff200190deaf5?width=600&format=jpeg&auto=webp",
                    ShortDescription = "interesting article about Elo Mau",
                    URL = "templateURL"
                },
                new ArticleHomeDto
                {
                    Title = "Elo Mau6",
                    PublicationDate = DateTime.Now,
                    Image =  "https://i.insider.com/64c7a2c9048ff200190deaf5?width=600&format=jpeg&auto=webp",
                    ShortDescription = "interesting article about Elo Mau",
                    URL = "templateURL"
                },
                new ArticleHomeDto
                {
                    Title = "Elo Mau7",
                    PublicationDate = DateTime.Now,
                    Image =  "https://i.insider.com/64c7a2c9048ff200190deaf5?width=600&format=jpeg&auto=webp",
                    ShortDescription = "interesting article about Elo Mau",
                    URL = "templateURL"
                },
                new ArticleHomeDto
                {
                    Title = "Elo Mau8",
                    PublicationDate = DateTime.Now,
                    Image =  "https://i.insider.com/64c7a2c9048ff200190deaf5?width=600&format=jpeg&auto=webp",
                    ShortDescription = "interesting article about Elo Mau",
                    URL = "templateURL"
                },
            };
            return Ok(list);

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
    }
}
