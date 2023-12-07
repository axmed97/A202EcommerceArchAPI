using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFSpecificationDAL : EFRepositoryBase<Specification, AppDbContext>, ISpecificationDAL
    {
        public void AddSpecification(int productId, List<Specification> specifications)
        {
            using var context = new AppDbContext();

            var result = specifications
                .Select(x =>
                {
                    x.ProductId = productId;
                    x.CreatedDate = DateTime.Now;
                    return x;
                }).ToList();

            context.Specifications.AddRange(result);
            context.SaveChanges();
        }
    }
}
