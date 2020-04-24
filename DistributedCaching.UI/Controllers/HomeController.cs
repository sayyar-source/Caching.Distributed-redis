using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DistributedCaching.UI.Models;
using ApplicationCore.Interfaces;
using DistributedCaching.Infrastructure.Data;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace DistributedCaching.UI.Controllers
{
    public class HomeController : Controller
    {
       private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private IDistributedCache _distributedCache;
        public HomeController(IUserRepository userRepository, IProductRepository productRepository, IDistributedCache distributedCache)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _distributedCache = distributedCache;
        }
        public IActionResult Index()
        {
           List<Product> products=null;
            var cachedProduct = _distributedCache.Get("product");//cache_product

            if (cachedProduct==null)
            {
                 products = GetProducts();
                var stringproduct = JsonConvert.SerializeObject(products);
                var bytes = Encoding.UTF8.GetBytes(stringproduct);
                _distributedCache.Set("product", bytes
                    , new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(500)
                    });
                return Ok(products);
            }
            else
            {
                var bytesAsString = Encoding.UTF8.GetString(cachedProduct);
                products = JsonConvert.DeserializeObject<List<Product>>(bytesAsString);
                return Ok(products);
            }
       
            
        }
        public List<Product> GetProducts()
        {
           var list= _productRepository.GetProducts();
            return list;
        }
      

        
    }
}
