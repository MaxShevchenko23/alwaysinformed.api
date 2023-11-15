using Microsoft.AspNetCore.Mvc;
using alwaysinformed_bll.Models.POST;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Services;
using alwaysinformed.Validation;
using alwaysinformed_bll.Models.UPDATE;

namespace alwaysinformed.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly ArticleService service;

    public ArticleController(ArticleService service)
    {
        this.service = service;
    }
    [HttpGet("get/id")]
    public async Task<ActionResult> ArticleGetByIdAsync([FromQuery] int id)
    {
        var article = await service.GetByIdAsync(id) ?? throw new APIException("ArgumentCannotBeNull");
        return Ok(article);
    }

    [HttpGet("get/short")]
    public async Task<ActionResult<IEnumerable<ArticleGetShortDto>>> GetAllShortArticlesAsync()
    {
        var dtos = await service.GetAllShortAsync() ?? throw new APIException();
        return Ok(dtos);
    }

    [HttpGet("get/full")]
    public async Task<ActionResult<IEnumerable<ArticleGetFullDto>>> GetAllFullArticlesAsync()
    {
        var dtos = await service.GetAllAsync() ?? throw new APIException();
        return Ok(dtos);
    }

    [HttpGet("get/full/first")]
    public async Task<ActionResult<IEnumerable<ArticleGetFullDto>>> GetFullFirstArticlesAsync([FromQuery]int n)
    {
        var articles = await service.GetFullFirstNRecords(n);
        return Ok(articles);
    }

    [HttpGet("get/short/first")]
    public async Task<ActionResult<IEnumerable<ArticleGetShortDto>>> GetShortFirstArticlesAsync([FromQuery] int n)
    {
        var articles = await service.GetShortFirstNRecords(n);
        return Ok(articles);
    }

    [HttpGet("get/full/last")]
    public async Task<ActionResult<IEnumerable<ArticleGetFullDto>>> GetFullLastArticlesAsync([FromQuery] int n)
    {
        var articles = await service.GetFullLastNRecords(n);
        return Ok(articles);
    }

    [HttpGet("get/short/last")]
    public async Task<ActionResult<IEnumerable<ArticleGetShortDto>>> GetShortLastArticlesAsync([FromQuery] int n)
    {
        var articles = await service.GetShortLastNRecords(n);
        return Ok(articles);
    }

    [HttpGet("get/full/url")]
    public async Task<ActionResult<ArticleGetShortDto>> GetFullArticleByURLAsync([FromQuery] string url)
    {
        var article = await service.GetArticleByURL(url);
        if (article == null)
            return NotFound();
        return Ok(article);
    }

    [HttpPost]
    public async Task<ActionResult> ArticlePostAsync([FromBody] ArticlePostDto model)
    {
        await service.AddAsync(model);
        return Ok();
    }
    [HttpDelete]
    public async Task<ActionResult> ArticleDeleteByIdAsync([FromQuery] int id)
    {
        await service.DeleteByIdAsync(id);
        return Ok();
    }
    [HttpPut]
    public async Task<ActionResult> ArticleUpdate([FromBody] ArticleUpdateDto model)
    {
        await service.UpdateAsync(model);
        return Ok();
    }




}
