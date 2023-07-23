using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderEvents.Common.Add
{
    public class OrderEventAddCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
