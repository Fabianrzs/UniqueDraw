using AutoMapper;
using UniqueDraw.Domain.Attributes;
using UniqueDraw.Domain.Ports.Helpers;

namespace UniqueDraw.Infrastructure.Adapters.Helpers;

[DomainService]
public class MappingService(IMapper mapper) : IMappingService
{
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return mapper.Map<TSource, TDestination>(source);
    }
    public TDestination Map<TDestination>(object source)
    {
        return mapper.Map<TDestination>(source);
    }
    ICollection<TDestination> IMappingService.Map<TSource, TDestination>(IEnumerable<TSource> source)
    {
        return mapper.Map<IEnumerable<TSource>, ICollection<TDestination>>(source);
    }

    ICollection<TDestination> IMappingService.Map<TDestination>(IEnumerable<object> source)
    {
        return mapper.Map<ICollection<TDestination>>(source);
    }
}