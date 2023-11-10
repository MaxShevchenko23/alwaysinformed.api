﻿using alwaysinformed.Validation;
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
    public class ComentController : ControllerBase
    {
        private readonly CommentService service;

        public ComentController(CommentService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentGetDto>>> GetAllCategoriesAsync()
        {
            var dtos = await service.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("get")]
        public async Task<ActionResult<CommentGetDto>> GetCommentById([FromQuery] int id)
        {
            var article = await service.GetByIdAsync(id) ?? throw new APIException("ArgumentCannotBeNull");
            return Ok(article);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ArticleGetShortDto>>> CommentPostAsync([FromBody] CommentPostDto dto)
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
        public async Task<ActionResult> GetShortLastArticlesAsync([FromBody] CommentUpdateDto dto)
        {
            await service.UpdateAsync(dto);
            return Ok();
        }
    }
}