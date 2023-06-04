using Application.Features.OrderEvents.Common.Add;
using Application.Features.OrderEvents.Queries.GetAll;
using Application.Features.Orders.Command.Add;
using Application.Features.Orders.Queries.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderEventController : ApiBaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddAsync(OrderEventAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync(OrderEventGetAllQuery query)
        {

            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new OrderEventGetAllQuery(id, null)));
        }
    }
}
