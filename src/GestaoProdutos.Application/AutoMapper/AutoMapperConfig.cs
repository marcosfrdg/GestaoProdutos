using AutoMapper;
using GestaoProdutos.Application.Produto;
using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.Application.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => src.Status.GetDescription()));
            
            CreateMap<ProductDto, Product>();
        }
    }
}
