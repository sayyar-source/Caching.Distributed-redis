using ApplicationCore.Interfaces;
using DistributedCaching.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Service
{
    public class ProductService : IProductRepository
    {
        private readonly DatabaseContext _db;
        public ProductService(DatabaseContext db)
        {
            _db = db;
        }
        public List<Product> GetProducts()
        {
            return _db.Products.ToList();
        }
    }
}
