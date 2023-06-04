using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class ProductGetAllDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public ProductCrawlType ProductCrawlType { get; set; }

        public decimal Price { get; set; }

        public decimal? SalePrice { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
