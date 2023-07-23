using Application.Common.Interfaces;
using Application.Features.Orders.Command.Add;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderEvents.Common.Add
{
    public class OrderEventAddCommandHendler : IRequestHandler<OrderEventAddCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderEventAddCommandHendler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Response<Guid>> Handle(OrderEventAddCommand request, CancellationToken cancellationToken)
        {
            var orderEvent = new OrderEvent()
            {
               Id = request.Id,
               OrderId = request.OrderId,
               Status = request.Status,
               CreatedOn=DateTimeOffset.Now,
            };

            await _applicationDbContext.OrderEvents.AddAsync(orderEvent, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>("Order event succesfully added.", orderEvent.Id);
        }

    }
}

