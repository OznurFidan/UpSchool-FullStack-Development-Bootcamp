using Application.Features.OrderEvents.Queries.GetAll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetAll
{
    public class OrderGetAllQuery : IRequest<List<OrderGetAllDto>>
    {
        public Guid Id { get; set; }
        public bool? IsDeleted { get; set; }

        public OrderGetAllQuery(Guid Id, bool? isDeleted)
        {
            Id = Id;

            IsDeleted = isDeleted;
        }
    }
}
