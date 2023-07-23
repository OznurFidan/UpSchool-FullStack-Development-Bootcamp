using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Command.Add
{
    public class ProductAddCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public ProductCrawlType ProductCrawlType { get; set; }

        public decimal Price { get; set; }

        public decimal? SalePrice { get; set; }
    }
}
