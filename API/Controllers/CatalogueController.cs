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
    public class CatalogueController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CatalogueController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [Route("add")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public async Task<IActionResult> CreateCatalogueAsync([FromBody] CreateCatalogueCommand createPOStoreCommand)
        {
            return await ProcessRequest(createPOStoreCommand, (result) => new ObjectResult(result) { StatusCode = StatusCodes.Status201Created }, UnprocessableEntity);
        }

        [Route("update")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       
        public async Task<IActionResult> UpdateCatalogueAsync( [FromBody] UpdateCatalogueCommand updatePOCommand)
        {
            
            return await ProcessRequest(updatePOCommand, (result) => new ObjectResult(result) { StatusCode = StatusCodes.Status201Created }, UnprocessableEntity);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Route("delete")]
        public async Task<IActionResult> DeleteCatalogue([FromHeader] int id)
        {
            return await ProcessRequest(new DeleteCatalogueCommand(id),
                     (result) => new ObjectResult(result) { StatusCode = StatusCodes.Status200OK }, UnprocessableEntity);
        }

        [Route("list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllCatalogueAsync()
        {
           
            try
            {
                var result = await _mediator.Send(new ListAllCatalogueQuery());
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
