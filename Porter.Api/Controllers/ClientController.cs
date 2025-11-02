using Microsoft.AspNetCore.Mvc;
using Porter.Application.Services.Interfaces;
using Porter.Common;
using Porter.Dto;

namespace Porter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {

        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger, IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService;
        }

        [HttpGet("fetch-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _clientService.GetAll();

            if (result.IsFailure)
            {
                ErrorResponseDto error = new ErrorResponseDto(result.ErrorCode, result.ErrorMessage);

                return BadRequest(error);
            }

            return Ok(Result<IList<ResponseClientDto>>.Success(((Result<IList<ResponseClientDto>>)result).Response));
        }


        [HttpGet()]
        public async Task<IActionResult> Get(string? docto)
        {
            var result = await _clientService.GetByDocto(docto);

            if (result.IsFailure)
            {
                ErrorResponseDto error = new ErrorResponseDto(result.ErrorCode, result.ErrorMessage);

                if(result.ErrorCode == "404")
                    return NotFound(error);

                return BadRequest(error);
            }

            return Ok(Result<ResponseClientDto>.Success(((Result<ResponseClientDto>)result).Response));
        }


        [HttpPost(Name = "PostClient")]
        public async Task<IActionResult> RegisterClient(RequestRegisterClientDto registerClient)
        {
            
            var result = await _clientService.Register(registerClient);

            if (result.IsFailure)
            {
                ErrorResponseDto error = new ErrorResponseDto(result.ErrorCode, result.ErrorMessage);
                return BadRequest(error);
            }
            return StatusCode(StatusCodes.Status201Created, ((Result<ResponseClientDto>)result).Response);
        }
    }
}
