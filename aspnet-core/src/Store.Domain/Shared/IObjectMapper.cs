namespace Store.Shared;

public interface IObjectMapper
{
    TDestination Map<TDestination>(object source);
    TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source);
}
