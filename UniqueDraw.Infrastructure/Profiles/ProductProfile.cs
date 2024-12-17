using AutoMapper;
using UniqueDraw.Domain.Entities.UniqueDraw;
using UniqueDraw.Domain.Models.Create;
using UniqueDraw.Domain.Models.Response;

namespace UniqueDraw.Infrastructure.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductCreateDTO, Product>();
        CreateMap<Product, ProductResponseDTO>();
    }
}
