using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.OrderDTOs;
using Entities.DTOs.UserDTOs;

namespace DataAccess.Abstract
{
    public interface IOrderDAL : IRepositoryBase<Order>
    {
        void AddOrders(int userId, List<Order> orders);
        UserOrderDTO GetUserOrders(int userId);
    }
}
