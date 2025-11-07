using MediatR;
using Microsoft.AspNetCore.Mvc;
using Personal.Common.Domain;
using Porter.Application.Commands.Booking;
using Porter.Application.Queries.Booking;
using Porter.Dto;

namespace Porter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : CustomBaseController
    {

        
        public BookingController(ILogger<BookingController> logger, IMediator mediator) :base(logger, mediator)
        {
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var result = await _mediator.Send(new GetBookingByIdQuery() { Id = id });

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);
            }

            return Ok(Result<ResponseBookingDto>.Success(((Result<ResponseBookingDto>)result).Response));
        }


        [HttpPost(Name = "PostBooking")]
        public async Task<IActionResult> Register(RegisterBookingCommand registerBooking)
        {
            var result = await _mediator.Send(registerBooking);

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);

            }
            return StatusCode(StatusCodes.Status201Created, ((Result<ResponseBookingDto>)result).Response);
        }

        [HttpGet("fetch-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllBookingsQuery());

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);
            }

            return Ok(Result<IList<ResponseBookingDto>>.Success(((Result<IList<ResponseBookingDto>>)result).Response));
        }

        [HttpDelete(Name = "DeleteBooking")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await _mediator.Send(new DeleteBookingByIdCommand() { Id = id });
           
            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);

            }
            return Ok();
        }

        [HttpGet("fetch-by-room-period")]
        public async Task<IActionResult> GetBookingListByRoomAndPeriod([FromQuery] int roomId, DateTime startDate, DateTime endDate)
        {
            var result = await _mediator.Send(new GetBookingListByRoomAndPeriodQuery() { RoomId = roomId, StartDate = startDate, EndDate = endDate });


            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);
            }

            return Ok(Result<IList<ResponseBookingDto>>.Success(((Result<IList<ResponseBookingDto>>)result).Response));
        }

        [HttpPut(Name = "UpdateBooking")]
        public async Task<IActionResult> Update(UpdateBookingCommand updateBookingCommand)
        {

            var result = await _mediator.Send(updateBookingCommand);
            

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);

            }
            return StatusCode(StatusCodes.Status200OK, ((Result<ResponseBookingDto>)result).Response);
        }
    }
}
