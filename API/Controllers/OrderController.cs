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
using Microsoft.AspNetCore.WebUtilities;

namespace API.Online.MarketPlace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [Route("placeOrder")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderCommand  createOrderCommand)
        {
            return await ProcessRequest(createOrderCommand, (result) => new ObjectResult(result) { StatusCode = StatusCodes.Status201Created }, UnprocessableEntity);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("details/{id}")]
       
        public async Task<IActionResult> GetOrderDetailsAsync( int id)
        {
            var result = await _mediator.Send(new ListOrderByIdQuery(id));
            return result != null && result.Success ? (IActionResult)Ok(result) : UnprocessableEntity(result);
        }
        private async Task<IActionResult> ProcessRequest<T>(IRequest<ApiResponse<T>> request,
   Func<object, IActionResult> SucessCallback, Func<object, IActionResult> failureCallback)
        {
            var result = await _mediator.Send(request);
            return result != null && result.Success ? SucessCallback(result) : failureCallback(result);
        }
    }
}
