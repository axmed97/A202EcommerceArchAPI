using AutoMapper;
using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.OrderDTOs;
using Entities.DTOs.ProductDTOs;
using Entities.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDAL _orderDAL;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        public OrderManager(IOrderDAL orderDAL, IMapper mapper, IProductService productService, IUserService userService)
        {
            _orderDAL = orderDAL;
            _mapper = mapper;
            _productService = productService;
            _userService = userService;
        }

        public IResult CreateOrder(int userId, List<OrderCreateDTO> orderCreateDTOs)
        {
            var productIds = orderCreateDTOs.Select(x => x.ProductId).ToList();
            var quantities = orderCreateDTOs.Select(x => x.Quantity).ToList();
            var result = BusinessRules.CheckLogic(CheckProductStockCount(productIds));

            if (!result.Success)
                return new ErrorResult();

            var map = _mapper.Map<List<Order>>(orderCreateDTOs);

            _orderDAL.AddOrders(userId, map);
            var products = orderCreateDTOs.Select(x => new ProductDecrementQuantityDTO
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList();

            _productService.RemoveProductStockCount(products);
            return new SuccessResult();
        }

        public IDataResult<UserOrderDTO> GetUserOrders(int userId)
        {
            var result = _orderDAL.GetUserOrders(userId);

            return new SuccessDataResult<UserOrderDTO>(result);
        }

        private IResult CheckProductStockCount(List<int> producIds)
        {
            var result = _productService.CheckProductCount(producIds);
            if (result.Success)
                return new SuccessResult();

            return new ErrorResult();
        }


    }
}
