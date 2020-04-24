using ApplicationCore.Interfaces;
using DistributedCaching.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.Service
{
    public class UserService : IUserRepository
    {
        private readonly DatabaseContext _db;
        public UserService(DatabaseContext db)
        {
            _db = db;
        }
        public List<User> GetUsers()
        {
            var users = _db.Users.ToList();
            return users;
        }
    }
}
