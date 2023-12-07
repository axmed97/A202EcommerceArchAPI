using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.OrderDTOs;
using Entities.DTOs.UserDTOs;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFOrderDAL : EFRepositoryBase<Order, AppDbContext>, IOrderDAL
    {
        public void AddOrders(int userId, List<Order> orders)
        {
            using var context = new AppDbContext();
            var result = orders.Select(x =>
            {
                x.UserId = userId;
                x.OrderNumber = Guid.NewGuid().ToString().Substring(1, 10);
                x.OrderStatusS = OrderStatus.OnPending;
                x.CreatedDate = DateTime.Now;
                return x;
            }).ToList();

            context.Orders.AddRange(result);
            context.SaveChanges();
        }

        public UserOrderDTO GetUserOrders(int userId)
        {
            using var context = new AppDbContext();
            var result = context.AppUser
                .Where(x => x.Id == userId)
                .Select(x => new UserOrderDTO
                {
                    Email = x.Email,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname,
                    OrderUserDTOs = x.Orders.Select(z => new OrderUserDTO
                    {
                        ProductName = z.Product.ProductName,
                        Price = z.ProductPrice,
                        Quantity = z.ProductQuantity,
                        DeliveryAddress = z.DeliveryAddress
                    }).ToList()
                }).FirstOrDefault();

            return result;
        }
    }
}
