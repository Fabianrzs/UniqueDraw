namespace UniqueDraw.Domain.Ports.Helpers;

public interface IMappingService
{
    TDestination Map<TSource, TDestination>(TSource source);
    TDestination Map<TDestination>(object source);
    ICollection<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source);
    ICollection<TDestination> Map<TDestination>(IEnumerable<object> source);
}
