using AutoMapper;
using Store.Shared.Dependencies;
using Store.Shared.Validations;

namespace Store.Shared;

/// <summary>
/// An implementation of <see cref="IObjectMapper"/> using <see cref="AutoMapper"/>.
/// </summary>
public class AutoMapperObjectMapper : IObjectMapper, ISingletonDependency
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoMapperObjectMapper"/> class.
    /// </summary>
    /// <param name="mapper">The AutoMapper instance to use for mapping operations.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="mapper"/> is null.</exception>
    public AutoMapperObjectMapper(IMapper mapper)
    {
        ArgumentValidator.NotNull(mapper);
        _mapper = mapper;
    }

    /// <inheritdoc />
    public TDestination Map<TDestination>(object source)
    {
        ArgumentValidator.NotNull(source);
        return _mapper.Map<TDestination>(source);
    }

    /// <inheritdoc />
    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        ArgumentValidator.NotNull(source!);
        ArgumentValidator.NotNull(destination!);

        return _mapper.Map(source, destination);
    }

    /// <inheritdoc />
    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source)
    {
        ArgumentValidator.NotNull(source);
        return _mapper.ProjectTo<TDestination>(source);
    }
}