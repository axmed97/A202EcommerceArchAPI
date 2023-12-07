using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDAL : IRepositoryBase<Product>
    {
        Product GetProduct(int id);
        void RemoveProductCount(List<ProductDecrementQuantityDTO> productDecrementQuantityDTOs);
    }
}
