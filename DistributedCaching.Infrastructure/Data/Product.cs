using System;
using System.Collections.Generic;
using System.Text;

namespace DistributedCaching.Infrastructure.Data
{
   public class Product
    {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
    }
}
