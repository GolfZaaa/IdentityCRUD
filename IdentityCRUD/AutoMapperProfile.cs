using AutoMapper;
using IdentityCRUD.DTOs.ProductDto;
using IdentityCRUD.Models;

namespace IdentityCRUD
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductRequest>();
            CreateMap<ProductRequest, Product>();


            CreateMap<Product, ProductResponse>();
            CreateMap<ProductResponse, Product>();
        }

    }
}
