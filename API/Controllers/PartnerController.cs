using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Online.MarketPlace.Commands;
using Application.Online.MarketPlace.Queries;
using Application.Online.MarketPlace.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Online.MarketPlace.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PartnerController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [Route("add")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public async Task<IActionResult> CreatePartnerAsync([FromBody] CreatePartnerCommand createPOStoreCommand)
        {
            return await ProcessRequest(createPOStoreCommand, (result) => new ObjectResult(result) { StatusCode = StatusCodes.Status201Created }, UnprocessableEntity);
        }

        [Route("list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllPartnerAsync()
        {
          
            try
            {
                var result = await _mediator.Send(new ListAllPartnerQuery());
                if (result.Success)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        private async Task<IActionResult> ProcessRequest<T>(IRequest<ApiResponse<T>> request,
     Func<object, IActionResult> SucessCallback, Func<object, IActionResult> failureCallback)
        {
            var result = await _mediator.Send(request);
            return result != null && result.Success ? SucessCallback(result) : failureCallback(result);
        }
    }
  
}
