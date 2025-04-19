using AutoMapper;
using Store.Shared.Dependency;
using Store.Shared.Validations;

namespace Store.Shared;

public class AutoMapperObjectMapper : IObjectMapper, ISingletonDependency
{
    private readonly IMapper _mapper;

    public AutoMapperObjectMapper(IMapper mapper)
    {
        ArgumentValidator.NotNull(mapper);
        _mapper = mapper;
    }

    public TDestination Map<TDestination>(object source)
    {
        ArgumentValidator.NotNull(source);
        return _mapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        ArgumentValidator.NotNull(source!);
        ArgumentValidator.NotNull(destination!);

        return _mapper.Map(source, destination);
    }

    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source)
    {
        ArgumentValidator.NotNull(source);
        return _mapper.ProjectTo<TDestination>(source);
    }
}