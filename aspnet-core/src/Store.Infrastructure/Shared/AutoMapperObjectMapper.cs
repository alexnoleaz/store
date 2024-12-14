using AutoMapper;
using Store.Shared.Dependency;

namespace Store.Shared;

/// <summary>
/// Provides an implementation of <see cref="IObjectMapper"/> using AutoMapper.
/// </summary>
public class AutoMapperObjectMapper : IObjectMapper, ISingletonDependency
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoMapperObjectMapper"/> class.
    /// </summary>
    /// <param name="mapper">The AutoMapper <see cref="IMapper"/> instance to use.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="mapper"/> is <c>null</c>.
    /// </exception>
    public AutoMapperObjectMapper(IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        _mapper = mapper;
    }

    /// <inheritdoc />
    public TDestination Map<TDestination>(object source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return _mapper.Map<TDestination>(source);
    }

    /// <inheritdoc />
    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return _mapper.Map(source, destination);
    }

    /// <inheritdoc />
    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));
        return _mapper.ProjectTo<TDestination>(source);
    }
}
