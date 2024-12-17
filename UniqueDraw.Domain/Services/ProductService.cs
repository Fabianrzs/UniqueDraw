using UniqueDraw.Domain.Entities.UniqueDraw;
using UniqueDraw.Domain.Ports.Persistence;
using UniqueDraw.Domain.Models.Response;
using UniqueDraw.Domain.Ports.Helpers;
using UniqueDraw.Domain.Models.Create;
using UniqueDraw.Domain.Attributes;

namespace UniqueDraw.Domain.Services;

[DomainService]
public class ProductService(IRepository<Product> repository, 
    IMappingService mapper)
{
    public async Task<ProductResponseDTO> CreateProductAsync(ProductCreateDTO productDto)
    {
        var product = mapper.Map<Product>(productDto);
        await repository.AddAsync(product);
        return mapper.Map<Product, ProductResponseDTO>(product);
    }

    public async Task<ICollection<ProductResponseDTO>> GetProductsByClientIdAsync(Guid clientId)
    {
        var products = await repository.FindAsync(p => p.ClientId == clientId);
        return mapper.Map<ProductResponseDTO>(products);
    }
}
