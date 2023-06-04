using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderEvents.Queries.GetAll
{
    public class OrderEventsGetAllQueryHandlerr : IRequestHandler<OrderEventGetAllQuery, List<OrderEventGetAllDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrderEventsGetAllQueryHandlerr(IApplicationDbContext applicationDbContext)
        {
              _applicationDbContext = applicationDbContext;
        }



        public async Task<List<OrderEventGetAllDto>> Handle(OrderEventGetAllQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _applicationDbContext.OrderEvents.AsQueryable();

            dbQuery = dbQuery.Where(x => x.OrderId == request.OrderId);

            if (request.IsDeleted.HasValue) dbQuery = dbQuery.Where(x => x.IsDeleted == request.IsDeleted.Value);

            dbQuery = dbQuery.Include(x => x.Order);

            var orderEvents = await dbQuery
                .Select(x => MapToDto(x))
                .ToListAsync(cancellationToken);

            return orderEvents.ToList();
        }

        private static OrderEventGetAllDto MapToDto(OrderEvent orderEvent)
        {
            return new OrderEventGetAllDto()
            {
                Id = orderEvent.Id,
                OrderId = orderEvent.OrderId,
               Status = orderEvent.Status,
               IsDeleted = orderEvent.IsDeleted,

            };
        }

        //private IEnumerable<OrderEventGetAllDto> MapOrderEventsToGetAllDtos(List<OrderEvent> orderEvents)
        //{
        //    List<OrderEventGetAllDto> ordereventGetAllDtos = new List<OrderEventGetAllDto>();

        //    foreach (var orderevent in orderEvents)
        //    {

        //        yield return new OrderEventGetAllDto()
        //        {
        //            Id = orderEvents.Id,
        //            OrderId = orderEvents.OrderId,
        //            Status = orderEvents.Status,
        //            IsDeleted = orderEvents.IsDeleted,

        //        };
        //    }
        //}
    }
}
