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

namespace Application.Features.Products.Queries.GetAll
{
    public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, List<ProductGetAllDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductGetAllQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<ProductGetAllDto>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            var dbQuery = _applicationDbContext.Products.AsQueryable();

            dbQuery = dbQuery.Where(x => x.OrderId == request.OrderId);

            if (request.IsDeleted.HasValue) dbQuery = dbQuery.Where(x => x.IsDeleted == request.IsDeleted.Value);

            dbQuery = dbQuery.Include(x => x.Order);

            var product = await dbQuery
                .Select(x => MapToDto(x))
                .ToListAsync(cancellationToken);

            return product.ToList();
        }

        private static ProductGetAllDto MapToDto(Product product)
        {
            return new ProductGetAllDto()
            {
                Id = product.Id,
                OrderId = product.OrderId,
                Name= product.Name,
                Picture= product.Picture,
                Price= product.Price,
                SalePrice= product.SalePrice,
                ProductCrawlType= product.ProductCrawlType,
                IsDeleted = product.IsDeleted,

            };
        }
    }
}
