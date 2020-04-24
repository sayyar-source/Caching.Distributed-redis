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
using Microsoft.Extensions.Caching.Memory;

namespace DistributedCaching.UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        //private IDistributedCache _distributedCache;
        private readonly IMemoryCache _memoryCache;
        public HomeController(IUserRepository userRepository, IProductRepository productRepository, IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {

            return View();

        }
        [HttpPost("edituser")]
        public IActionResult EditUser(User user)
        {
            user.Id =Guid.Parse("0e45873d-3da2-4775-8cb5-08d7e850c7c4");
            // update database
            _userRepository.EditUser(user);

            //update cache
            IList<User> cachedUsers;
            List<User> usersList = new List<User>();
            if (_memoryCache.TryGetValue("users", out cachedUsers))
                {
                var editUser = cachedUsers.FirstOrDefault(u => u.Id == user.Id);
                editUser.PhoneNumber = user.PhoneNumber;
                editUser.Password = user.Password;
                usersList.Add(editUser);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                     .SetPriority(CacheItemPriority.NeverRemove)
                     .SetAbsoluteExpiration(TimeSpan.FromDays(1));
                //.SetSlidingExpiration(TimeSpan.FromSeconds(3))
                // .RegisterPostEvictionCallback(UsersCacheEvicted);
                _memoryCache.Set("users", usersList, cacheEntryOptions);
               
            }
            else
            {
                var editUser = _userRepository.GetById(user.Id);
                usersList.Add(editUser);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetPriority(CacheItemPriority.NeverRemove)
                    .SetAbsoluteExpiration(TimeSpan.FromDays(1));
            
                _memoryCache.Set("users", usersList, cacheEntryOptions);
            }
           
            return RedirectToAction("Index");

        }




    }
}
