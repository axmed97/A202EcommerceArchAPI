using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;
        private readonly IMapper _mapper;
        public ProductManager(IProductDAL productDAL, IMapper mapper)
        {
            _productDAL = productDAL;
            _mapper = mapper;
        }

        public IResult AddProduct(ProductCreateDTO productCreateDTO)
        {
            var map = _mapper.Map<Product>(productCreateDTO);
            _productDAL.Add(map);
            return new SuccessResult("Product created Successfully");
        }

        public IResult ChangeProductStatus(int productId, bool status)
        {
            var product = _productDAL.Get(x => x.Id == productId);
            product.Status = status;
            _productDAL.Update(product);
            return new SuccessResult("Product status changed!");

        }

        public IResult DeleteProduct(int productId)
        {
            var product = _productDAL.Get(x => x.Id == productId);
            _productDAL.Delete(product);
            return new SuccessResult("Deleted successfully!");
        }

        public IDataResult<List<ProductFilterDTO>> FilterProductsList(int categoryId, decimal minPrice, decimal maxPrice)
        {
            var producs = _productDAL
                .GetAll(x => x.CategoryId == categoryId && x.Price >= minPrice && x.Price <= maxPrice);

            var map = _mapper.Map<List<ProductFilterDTO>>(producs);
            return new SuccessDataResult<List<ProductFilterDTO>>(map);
        }

        public IResult UpdateProduct(ProductUpdateDTO productUpdateDTO)
        {
            var map = _mapper.Map<Product>(productUpdateDTO);

            _productDAL.Update(map);
            return new SuccessResult("Product updated successfully");

        }
    }
}
