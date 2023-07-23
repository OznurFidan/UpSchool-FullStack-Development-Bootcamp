using Application.Common.Models.WorkerService;
using Application.Features.Orders.Command.Add;
using Application.Features.Orders.Queries.GetAll;
using Application.Features.Orders.Queries.GetById;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ApiBaseController

    {
       
        [HttpPost]
        public async Task<IActionResult> AddAsync(OrderAddCommand command)
        {
            //var order = new Order()
            //{
            //    Id = command.Id,
            //    ProductCrawlType = command.ProductCrawlType,
            //    //RequestedAmount = command.RequestedAmount,
            //    //TotalFoundAmount = command.TotalFoundAmount,
                

            //};

            return Ok(await Mediator.Send(command));
        }

        [HttpPost("CrawlerServiceExample")]
        public async Task<IActionResult> CrawlerServiceExampleAsync(WorkerServiceNewOrderAddedDto newOrderAddedDto)
        {
            return Ok(await Mediator.Send(newOrderAddedDto));
        }

        [HttpPost("GetAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync(OrderGetAllQuery query)
        {
            var order= await Mediator.Send(query);
            if (order == null) { return NotFound(); }

            return Ok(order);
        }

        [HttpGet("{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new OrderGetByIdQuery(id)));
        }



    }
}
