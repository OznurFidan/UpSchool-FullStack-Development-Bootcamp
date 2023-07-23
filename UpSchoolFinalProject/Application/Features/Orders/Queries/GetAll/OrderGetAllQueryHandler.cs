using Application.Common.Interfaces;
using Application.Features.OrderEvents.Queries.GetAll;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetAll
{
    public class OrderGetAllQueryHandler : IRequestHandler<OrderGetAllQuery, List<OrderGetAllDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderGetAllQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<OrderGetAllDto>> Handle(OrderGetAllQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _applicationDbContext.Orders.AsQueryable();

            //dbQuery=dbQuery.Where(x=>x.Id == request.Id);
            if (request.IsDeleted.HasValue) dbQuery = dbQuery.Where(x => x.IsDeleted == request.IsDeleted.Value);

            var order = await dbQuery
              .Select(x=>MapToDto(x))
              .ToListAsync(cancellationToken);

            return order.ToList();
        }

        private static OrderGetAllDto MapToDto(Order order)
        {
            return new OrderGetAllDto()
            {
                Id = order.Id,
                RequestedAmount=order.RequestedAmount,
                TotalFoundAmount=order.TotalFoundAmount,
                //ProductCrawlType=order.ProductCrawlType,
                IsDeleted = order.IsDeleted,

            };
        }
    }
}
