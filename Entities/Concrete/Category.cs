using Core.Entities;
using Entities.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string PhotoUrl { get; set; }
        public List<Product> Products { get; set; }
    }
}
