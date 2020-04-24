using DistributedCaching.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
   public interface IUserRepository
    {
        List<User> GetUsers();
    }
}
