using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Command.Add
{
    public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public ProductAddCommandHandler(IApplicationDbContext applicationDbContext)
        {
             _applicationDbContext = applicationDbContext;
        }


        public async Task<Response<Guid>> Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            var product = new Product()
            {
                Id=request.Id,
                OrderId = request.OrderId,
                Name = request.Name,
                Picture = request.Picture,
                Price = request.Price,
                ProductCrawlType = request.ProductCrawlType,
                SalePrice = request.SalePrice,
                CreatedOn=DateTimeOffset.Now,
            };

            await _applicationDbContext.Products.AddAsync(product, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>("Product succesfully added.", product.Id);
        }
    }
}
