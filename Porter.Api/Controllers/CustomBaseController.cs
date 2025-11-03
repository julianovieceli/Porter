using MediatR;
using Microsoft.AspNetCore.Mvc;
using Porter.Common;
using Porter.Dto;

namespace Porter.Api.Controllers
{
    public abstract class CustomBaseController: ControllerBase
    {

        protected readonly ILogger<CustomBaseController> _logger;
        protected readonly IMediator _mediator;


        protected CustomBaseController(ILogger<CustomBaseController> logger, IMediator mediator)

        {
            _logger = logger;
            _mediator = mediator;
        }

        protected IActionResult CreateResponseFromResult(Result result)
        {
            ErrorResponseDto error = new ErrorResponseDto(result.ErrorCode, result.ErrorMessage);

            if (result.ErrorCode == "404")
                return NotFound(error);

            return BadRequest(error);
        }
    }
}
