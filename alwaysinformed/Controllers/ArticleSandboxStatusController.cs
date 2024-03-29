﻿using alwaysinformed.Validation;
using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace alwaysinformed.Controllers
{
    //[Authorize]
    [Route("api/sandboxstatus")]
    [ApiController]
    public class ArticleSandboxStatusController : ControllerBase
    {
        private readonly ArticleSandboxStatusService service;

        public ArticleSandboxStatusController(ArticleSandboxStatusService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleSandboxStatusGetDto>>> GetAllStatusesAsync()
        {
           var statuses = await service.ArticleSandboxStatusGetDtoAllAsync() ?? throw new APIException();
            return Ok(statuses);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ArticleSandboxStatusGetDto>> GetStatusByIdAsync([FromQuery]int id)
        {
            var status = await service.ArticleSandboxStatusGetDtoByIdAsync(id) ?? throw new APIException();
            return Ok(status);
        }

        
    }
}
