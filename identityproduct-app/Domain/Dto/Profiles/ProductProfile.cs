using AutoMapper;
using identityproduct_app.Domain.Dto.Create;
using identityproduct_app.Domain.Dto.Read;
using identityproduct_app.Domain.Models;

namespace identityproduct_app.Domain.Dto.Profiles
{
    public class ProductProfile : Profile
    {

        public ProductProfile()
        {
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductReadDto>();
        }
    }
}
