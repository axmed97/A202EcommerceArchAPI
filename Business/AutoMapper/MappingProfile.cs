using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Entities.DTOs.ProductDTOs;
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
        }
    }
}
