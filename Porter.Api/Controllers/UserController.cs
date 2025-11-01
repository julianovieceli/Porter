using Microsoft.AspNetCore.Mvc;
using Porter.Application.Services.Interfaces;
using Porter.Common;
using Porter.Dto;
using System.Collections.Generic;

namespace Porter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserPorterService _userPorterService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserPorterService userPorterService)
        {
            _logger = logger;
            _userPorterService = userPorterService;
        }

        [HttpGet("fetch-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userPorterService.GetAll();

            if (result.IsFailure)
            {
                ErrorResponseDto error = new ErrorResponseDto(result.ErrorCode, result.ErrorMessage);

                return BadRequest(error);
            }

            return Ok(Result<IList<ResponseUserPorterDto>>.Success(((Result<IList<ResponseUserPorterDto>>)result).Value));
        }
    }
}
