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

        public bool EditUser(User user)
        {
            try
            {
                _db.Users.Update(user);
                _db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
        }

        public User GetById(Guid id)
        {
            return _db.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            var users = _db.Users.ToList();
            return users;
        }
    }
}
