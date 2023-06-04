using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;

namespace Application.Features.Orders.Command.Add
{
    public class OrderAddCommandHendler : IRequestHandler<OrderAddCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderAddCommandHendler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        
        public async Task<Response<Guid>> Handle(OrderAddCommand request, CancellationToken cancellationToken)
        {
            var order = new Order()
            {
                Id = request.Id,
                RequestedAmount = request.RequestedAmount,
                TotalFoundAmount = request.TotalFoundAmount,
                ProductCrawlType = request.ProductCrawlType,
            };

            await _applicationDbContext.Orders.AddAsync(order, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var hubConnection = new HubConnectionBuilder()
              .WithUrl($"https://localhost:7296/Hubs/SeleniumLogHub")
              .WithAutomaticReconnect()
            .Build();

            return new Response<Guid>("Order succesfully added.", order.Id);
        }

    }
}
