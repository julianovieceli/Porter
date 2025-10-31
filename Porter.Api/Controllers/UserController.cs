using Microsoft.AspNetCore.Mvc;
using Porter.Application.Services.Interfaces;

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

            if (result.Count == 0)
            {
                _logger.LogInformation("No results");
                return NotFound();
            }
            _logger.LogInformation("Ok");
            return Ok(result);
        }
    }
}
