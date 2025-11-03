using MediatR;
using Microsoft.AspNetCore.Mvc;
using Porter.Application.Services.Interfaces;
using Porter.Common;
using Porter.Dto;

namespace Porter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : CustomBaseController
    {

        private readonly IRoomService _roomService;
        
        public RoomController(ILogger<ClientController> logger, IRoomService roomService, IMediator mediator) :base(logger, mediator)
        {
            _roomService = roomService;
        }

        [HttpGet("fetch-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roomService.GetAll();

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);
            }

            return Ok(Result<IList<ResponseRoomDto>>.Success(((Result<IList<ResponseRoomDto>>)result).Response));
        }


        [HttpGet()]
        public async Task<IActionResult> Get(string? name)
        {
            var result = await _roomService.GetByName(name);

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);
            }

            return Ok(Result<ResponseRoomDto>.Success(((Result<ResponseRoomDto>)result).Response));
        }


        [HttpPost(Name = "PostRoom")]
        public async Task<IActionResult> Register(RequestRegisterRoomDto registerRoom)
        {
            
            var result = await _roomService.Register(registerRoom);

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);

            }
            return StatusCode(StatusCodes.Status201Created, ((Result<ResponseRoomDto>)result).Response);
        }
    }
}
