using MediatR;
using Microsoft.AspNetCore.Mvc;
using Porter.Application.Commands.Booking;
using Porter.Application.Queries.Room;
using Porter.Common;
using Porter.Dto;

namespace Porter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : CustomBaseController
    {

        public RoomController(ILogger<ClientController> logger,  IMediator mediator) :base(logger, mediator)
        {
        }

        [HttpGet("fetch-all")]
        public async Task<IActionResult> GetAll()
        {
            //
            var result = await _mediator.Send(new GetAllRoomsQuery());

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);
            }

            return Ok(Result<IList<ResponseRoomDto>>.Success(((Result<IList<ResponseRoomDto>>)result).Response));
        }


        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetRoomByNameQuery getRoomByNameQuery)
        {
            var result = await _mediator.Send(getRoomByNameQuery);

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);
            }

            return Ok(Result<ResponseRoomDto>.Success(((Result<ResponseRoomDto>)result).Response));
        }


        [HttpPost(Name = "PostRoom")]
        public async Task<IActionResult> Register(RegisterRoomCommand registerRoom)
        {
            
            var result = await _mediator.Send(registerRoom);

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);

            }
            return StatusCode(StatusCodes.Status201Created, ((Result<ResponseRoomDto>)result).Response);
        }
    }
}
