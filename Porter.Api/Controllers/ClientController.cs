using MediatR;
using Microsoft.AspNetCore.Mvc;
using Personal.Common;
using Personal.Common.Domain;
using Porter.Application.Commands.Client;
using Porter.Application.Queries.Client;
using Porter.Dto;

namespace Porter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : CustomBaseController
    {

        
        public ClientController(ILogger<ClientController> logger, IMediator mediator) :base(logger, mediator)
        {
        }

        [HttpGet("fetch-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllClientsQuery());

            
            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);
            }

            return Ok(Result<IList<ResponseClientDto>>.Success(((Result<IList<ResponseClientDto>>)result).Response));
        }


        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] string docto)
        {
            var result = await _mediator.Send(new GetClientByDoctoQuery() { Docto = docto });

          
            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);
            }

            return Ok(Result<ResponseClientDto>.Success(((Result<ResponseClientDto>)result).Response));
        }


        [HttpPost(Name = "PostClient")]
        public async Task<IActionResult> Register(RegisterClientCommand registerClient)
        {
            var result = await _mediator.Send(registerClient);


            if (result.IsFailure)
            {
                return base.CreateResponseFromResult(result);

            }
            return StatusCode(StatusCodes.Status201Created, ((Result<ResponseClientDto>)result).Response);
        }
    }
}
