﻿using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : EntityBase<Guid>
    {

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public string Name { get; set; }

        public string Picture { get; set; }

        public ProductCrawlType ProductCrawlType { get; set; }

        public decimal Price { get; set; }

        public decimal? SalePrice { get; set; }

    }
}