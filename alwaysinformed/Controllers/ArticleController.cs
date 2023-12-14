using Microsoft.AspNetCore.Mvc;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Services;
using alwaysinformed.Validation;
using alwaysinformed_bll.Models.UPDATE;
//using Newtonsoft.Json;
using System.Text.Json;

namespace alwaysinformed.Controllers;

[Route("[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly ArticleService service;
    private readonly int maxPageSize = 20;
    public ArticleController(ArticleService service)
    {

        this.service = service;
    }
    [HttpGet("id")]
    public async Task<ActionResult<ArticleGetFullDto>> ArticleGetByIdAsync([FromQuery] int id)
    {
        var article = await service.GetByIdAsync(id);
        if (article == null)
        {
            return NotFound();
        }
        return Ok(article);
    }


    [HttpGet("full/latest")]
    public async Task<ActionResult<IEnumerable<ArticleGetFullDto>>> GetFullFirstArticlesAsync([FromQuery]int n)
    {
        if (n > maxPageSize)
            n = maxPageSize;
        var articles = await service.GetFullFirstNRecords(n);
        return Ok(articles);
    }

    [HttpGet("short/latest")]
    public async Task<ActionResult<IEnumerable<ArticleGetShortDto>>> GetShortFirstArticlesAsync([FromQuery] int n)
    {
        if (n > maxPageSize)
            n = maxPageSize;
        var articles = await service.GetShortFirstNRecords(n);
        return Ok(articles);
    }

    [HttpGet("full/oldest")]
    public async Task<ActionResult<IEnumerable<ArticleGetFullDto>>> GetFullLastArticlesAsync([FromQuery] int n)
    {
        if (n > maxPageSize)
            n = maxPageSize;
        var articles = await service.GetFullLastNRecords(n);
        return Ok(articles);
    }

    [HttpGet("short/oldest")]
    public async Task<ActionResult<IEnumerable<ArticleGetShortDto>>> GetShortLastArticlesAsync([FromQuery] int n)
    {
        if (n > maxPageSize)
            n = maxPageSize;
        var articles = await service.GetShortLastNRecords(n);
        return Ok(articles);
    }

    [HttpGet("full/url")]
    public async Task<ActionResult<ArticleGetShortDto>> GetFullArticleByURLAsync([FromQuery] string url)
    {
        var article = await service.GetArticleByURL(url);
        if (article == null)
            return NotFound();
        return Ok(article);
    }

    //[HttpPost]
    //public async Task<ActionResult> ArticlePostAsync([FromBody] ArticlePostDto model)
    //{
    //    try
    //    {
    //        await service.AddAsync(model);
    //        return Ok();
    //    }
    //    catch (Exception)
    //    {
    //        return BadRequest();
    //    }
        
    //}

    [HttpGet("filter")]
    public async Task<ActionResult<ArticleGetShortDto>> GetFilteredArticles([FromQuery] string? categoryName, 
        [FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? searchQuery, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
   
        if (pageSize > maxPageSize)
            pageSize = maxPageSize;

        var (articles,paginationMeta) = await service.GetFilteredArticles(categoryName, firstName, lastName, searchQuery,pageNumber,pageSize);

        HttpContext.Response.Headers.Add("page-current", $"{paginationMeta.currentPage}");
        HttpContext.Response.Headers.Add("page-size", $"{paginationMeta.pageSize}");
        HttpContext.Response.Headers.Add("page-total", $"{paginationMeta.totalPagesAmount}");
        HttpContext.Response.Headers.Add("items-count", $"{paginationMeta.totalItemCount}");

        return Ok(articles);
    }

    [HttpDelete]
    public async Task<ActionResult> ArticleDeleteByIdAsync([FromQuery] int id)
    {
        await service.DeleteByIdAsync(id);
        return NoContent();
    }
    [HttpPut]
    public async Task<ActionResult> ArticleUpdate([FromBody] ArticleUpdateDto model)
    {
        await service.UpdateAsync(model);
        var newArticle = await GetFullArticleByURLAsync(model.Url);
        return Ok(newArticle);
    }




}
