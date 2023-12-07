using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductDTOs;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDAL _productDAL;
        private readonly IMapper _mapper;
        private readonly ISpecificationService _specificationService;
        public ProductManager(IProductDAL productDAL, IMapper mapper, ISpecificationService specificationService)
        {
            _productDAL = productDAL;
            _mapper = mapper;
            _specificationService = specificationService;
        }

        public IResult AddProduct(ProductCreateDTO productCreateDTO)
        {
            var map = _mapper.Map<Product>(productCreateDTO);
            map.CreatedDate = DateTime.Now;
            _productDAL.Add(map);
            _specificationService.AddSpecificationProduct(map.Id, productCreateDTO.Specifications);
            return new SuccessResult("Product created Successfully");
        }

        public IResult ChangeProductStatus(int productId, bool status)
        {
            var product = _productDAL.Get(x => x.Id == productId);
            product.Status = status;
            _productDAL.Update(product);
            return new SuccessResult("Product status changed!");

        }

        public IDataResult<bool> CheckProductCount(List<int> productIds)
        {
            var products = _productDAL.GetAll(x => productIds.Contains(x.Id));

            if(products.Any(x => x.Quantity == 0))
                return new ErrorDataResult<bool>(false);
            else
                return new SuccessDataResult<bool>(true);
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
        public IDataResult<ProductGetDTO> GetProduct(int productId)
        {
            var product = _productDAL.GetProduct(productId);
            var map = _mapper.Map<ProductGetDTO>(product);
            map.CategoryName = product.Category.CategoryName;
            return new SuccessDataResult<ProductGetDTO>(map);
        }

        public IResult RemoveProductStockCount(List<ProductDecrementQuantityDTO> productDecrementQuantityDTOs)
        {
            _productDAL.RemoveProductCount(productDecrementQuantityDTOs);
            return new SuccessResult();
        }

        public IResult UpdateProduct(ProductUpdateDTO productUpdateDTO)
        {
            var map = _mapper.Map<Product>(productUpdateDTO);

            _productDAL.Update(map);
            return new SuccessResult("Product updated successfully");

        }
    }
}
