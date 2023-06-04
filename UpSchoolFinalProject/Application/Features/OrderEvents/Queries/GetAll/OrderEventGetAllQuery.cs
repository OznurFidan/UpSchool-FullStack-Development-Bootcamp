using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderEvents.Queries.GetAll
{
    public class OrderEventGetAllQuery : IRequest<List<OrderEventGetAllDto>>
    {
        public Guid OrderId { get; set; }
        public bool? IsDeleted { get; set; }

        public OrderEventGetAllQuery(Guid orderId, bool? isDeleted)
        {
           OrderId=orderId;

            IsDeleted = isDeleted;
        }
    }
}
