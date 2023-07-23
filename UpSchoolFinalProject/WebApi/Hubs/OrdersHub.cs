using Application.Common.Models.WorkerService;
using Application.Features.Orders.Command.Add;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class OrdersHub:Hub
    {
        private ISender? _mediator;
        private readonly IHttpContextAccessor _contextAccessor;

        public OrdersHub(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected ISender Mediator => _mediator ??= _contextAccessor.HttpContext.RequestServices.GetRequiredService<ISender>();


        [Authorize]
        public async Task<Guid> AddANewOrder(OrderAddCommand command)
        {
            var accessToken = Context.GetHttpContext().Request.Query["access_token"];
            var result = await Mediator.Send(command);

            

            await Clients.All.SendAsync("NewOrderAdded", new WorkerServiceNewOrderAddedDto(command, accessToken));

            return result.Data;
        }
    }
}

