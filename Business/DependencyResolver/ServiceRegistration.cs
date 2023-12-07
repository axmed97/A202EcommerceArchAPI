using AutoMapper;
using Business.Abstract;
using Business.AutoMapper;
using Business.Concrete;
using Core.Utilities.MailHelper;
using DataAccess.Abstract;
using DataAccess.Concrete.SQLServer;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDAL, EFUserDAL>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDAL, EFCategoryDAL>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDAL, EFProductDAL>();

            services.AddScoped<ISpecificationService, SpecificationManager>();
            services.AddScoped<ISpecificationDAL, EFSpecificationDAL>();

            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IOrderDAL, EFOrderDAL>();

            services.AddScoped<IEmailHelper, EmailHelper>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
