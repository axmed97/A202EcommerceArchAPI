using Business.Abstract;
using Entities.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [MapToApiVersion("2.0")]
        [HttpPost("[action]")]
        public IActionResult CreateProduct(ProductCreateDTO productCreateDTO)
        {
            var result = _productService.AddProduct(productCreateDTO);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut("[action]")]
        [MapToApiVersion("2.0")]
        public IActionResult UpdateProduct(ProductUpdateDTO productUpdateDTO)
        {
            var result = _productService.UpdateProduct(productUpdateDTO);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPatch("[action]")]
        [MapToApiVersion("2.0")]
        public IActionResult ChangeStatusProduct(int Id, bool status)
        {
            var result = _productService.ChangeProductStatus(Id, status);
            return Ok(result);
        }
        [HttpDelete("[action]")]
        [MapToApiVersion("2.0")]
        public IActionResult Delete(int Id)
        {
            var result = _productService.DeleteProduct(Id);
            return Ok(result);
        }
        [HttpGet("[action]")]
        [MapToApiVersion("2.0")]
        public IActionResult GetAllProductFilter(int categoryId, decimal minPrice, decimal maxPrice)
        {
            var result = _productService.FilterProductsList(categoryId, minPrice, maxPrice);
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("[action]/{productId}")]
        [MapToApiVersion("2.0")]
        public IActionResult GetProductDetail(int productId)
        {
            var result = _productService.GetProduct(productId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

    }
}
