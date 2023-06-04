﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class ProductGetAllQuery:IRequest<List<ProductGetAllDto>>
    {
        public Guid OrderId { get; set; }
        public bool? IsDeleted { get; set; }

        public ProductGetAllQuery(Guid orderId, bool? isDeleted)
        {
            OrderId = orderId;

            IsDeleted = isDeleted;
        }
    }
}