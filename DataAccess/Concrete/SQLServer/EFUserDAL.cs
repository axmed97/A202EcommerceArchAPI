using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFUserDAL : EFRepositoryBase<User, AppDbContext>, IUserDAL
    {
        public User GetUserOrders(int userId)
        {
            using var context = new AppDbContext();

            var user = context.AppUser
                .Include(x => x.Orders)
                .FirstOrDefault(x => x.Id == userId);

            return user;
        }
    }
}
