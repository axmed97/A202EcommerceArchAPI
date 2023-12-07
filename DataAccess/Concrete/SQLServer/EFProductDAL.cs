using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFProductDAL : EFRepositoryBase<Product, AppDbContext>, IProductDAL
    {
        public Product GetProduct(int id)
        {
            using var context = new AppDbContext();

            var result = context.Products
                .Include(x => x.Specifications)
                .Include(x => x.Category)
                .SingleOrDefault(x => x.Id == id);

            return result;
        }

        public void RemoveProductCount(List<ProductDecrementQuantityDTO> productDecrementQuantityDTOs)
        {
            using var context = new AppDbContext();

            for (int i = 0; i < productDecrementQuantityDTOs.Count; i++)
            {
                var product = context.Products.FirstOrDefault(x => x.Id == productDecrementQuantityDTOs[i].ProductId);
                product.Quantity -= productDecrementQuantityDTOs[i].Quantity;
                context.Products.Update(product);
                context.SaveChanges();
            }
        }
    }
}
