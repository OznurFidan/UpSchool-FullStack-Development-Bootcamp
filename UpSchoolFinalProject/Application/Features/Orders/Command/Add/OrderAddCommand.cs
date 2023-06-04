using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Orders.Command.Add
{
    public class OrderAddCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public int RequestedAmount { get; set; }

        public int TotalFoundAmount { get; set; }

        public ProductCrawlType ProductCrawlType { get; set; }

    }
}
