using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISpecificationDAL : IRepositoryBase<Specification>
    {
        void AddSpecification(int productId, List<Specification> specifications);
    }
}
