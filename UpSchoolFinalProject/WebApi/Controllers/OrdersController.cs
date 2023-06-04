using Application.Features.Orders.Command.Add;
using Application.Features.Orders.Queries.GetAll;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ApiBaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddAsync(OrderAddCommand command, CancellationToken cancellationToken)
        {
            var order = new Order()
            {
                Id = command.Id,
                RequestedAmount = command.RequestedAmount,
                TotalFoundAmount = command.TotalFoundAmount,

            };



            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync(OrderGetAllQuery query)
        {

            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new OrderGetAllQuery(id, null)));
        }
    }
}
