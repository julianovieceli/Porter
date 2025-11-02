using Microsoft.AspNetCore.Mvc;
using Porter.Application.Services;
using Porter.Application.Services.Interfaces;
using Porter.Common;
using Porter.Dto;

namespace Porter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : CustomBaseController
    {

        private readonly IBookingService _bookingSrevice;
        
        public BookingController(ILogger<BookingController> logger, IBookingService bookingSrevice) :base(logger)
        {
            _bookingSrevice = bookingSrevice;
        }

        [HttpGet()]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _bookingSrevice.GetById(id);

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);
            }

            return Ok(Result<ResponseBookingDto>.Success(((Result<ResponseBookingDto>)result).Response));
        }


        [HttpPost(Name = "PostBooking")]
        public async Task<IActionResult> Register(RequestRegisterBookingDto registerBooking)
        {
            
            var result = await _bookingSrevice.Register(registerBooking);

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);

            }
            return StatusCode(StatusCodes.Status201Created, ((Result<ResponseBookingDto>)result).Response);
        }

        [HttpGet("fetch-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookingSrevice.GetAll();

            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);
            }

            return Ok(Result<IList<ResponseBookingDto>>.Success(((Result<IList<ResponseBookingDto>>)result).Response));
        }
    }
}
