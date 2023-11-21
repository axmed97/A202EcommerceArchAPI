using Core.Entities;
using Entities.Comman;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order : BaseEntity, IEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductPrice { get; set; }
        public string DeliveryAddress { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus OrderStatusS { get; set; }
    }
}
