using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.OrderDTOs;
using Entities.DTOs.ProductDTOs;
using Entities.DTOs.SpecificationDTOs;
using Entities.DTOs.UserDTOs;

namespace Business.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, User>();

            CreateMap<CategoryAddDTO, Category>();

            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();
            CreateMap<Product, ProductFilterDTO>();
            CreateMap<Product, ProductGetDTO>();

            CreateMap<SpecificationAddDTO, Specification>();
            CreateMap<Specification, SpecificationListDTO>();

            CreateMap<OrderCreateDTO, Order>();

            //CreateMap<Order, OrderUserDTO>()
            //    .ForMember(x => x.ProductName, z => z.MapFrom(y => y.Product.ProductName))
            //    .ForMember(x => x.Price, z => z.MapFrom(y => y.Product.Price))
            //    .ForMember(x => x.OrderStatus, z => z.MapFrom(y => Enum.GetName(y.OrderStatusS)));

            //CreateMap<User, UserOrderDTO>()
            //    .ForMember(x => x.OrderUserDTOs, z => z.MapFrom(y => y.Orders));

        }
    }
}
