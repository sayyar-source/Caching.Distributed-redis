using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DistributedCaching.Infrastructure.Data
{
   public class CatalogContextSeed
    {
        public static Task SeedDB(DatabaseContext context)
        {
			try
			{
                if(!context.Users.Any())
                {
                    context.Users.AddRange(GetUsers());
                     context.SaveChanges();
                }
                if(!context.Products.Any())
                {
                    context.Products.AddRange(Getproduct());
                    context.SaveChanges();
                }
			}
			catch (Exception)
			{

				throw;
			}
            return Task.CompletedTask;
        }
        static IEnumerable<User> GetUsers()
        {
            return new List<User>()
            {
                new User
                {
                    Email="admin@gmail.com",
                    FirstName="ahmet",
                    LastName="kaya",
                    Password="123",
                    PhoneNumber="555555"
                }
               
            };
        }
        static IEnumerable<Product> Getproduct()
        {
            Product[] products = new Product[]
        {
            new Product { Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product {  Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };
            return products;
        
        }
    }
}
