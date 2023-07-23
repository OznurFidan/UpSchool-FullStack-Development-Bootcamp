using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Orders.Command.Add
{
    public class OrderAddCommandHendler : IRequestHandler<OrderAddCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public OrderAddCommandHendler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
        }


        public async Task<Response<Guid>> Handle(OrderAddCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request.RequestedAmount);
            var id =Guid.NewGuid();
            var order = new Order()
            {
                Id = id,
                UserId = "_currentUserService.UserId",
                RequestedAmount = request.RequestedAmount,
                TotalFoundAmount = request.TotalFoundAmount,
                ProductCrawlType = request.ProductCrawlType,
                CreatedOn=DateTimeOffset.Now,

                OrderEvents = new List<OrderEvent>()
                {
                    new OrderEvent()
                    {
                        Id= Guid.NewGuid(),
                        OrderId=id,
                        Status=OrderStatus.BotStarted
                    }
                 }
            };

           

            await _applicationDbContext.Orders.AddAsync(order, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            //var hubConnection = new HubConnectionBuilder()
            //  .WithUrl($"https://localhost:7196/Hubs/OrdersHub")
            //  .WithAutomaticReconnect()
            //.Build();

            return new Response<Guid>("Order succesfully added.", order.Id);
        }

    }
}
