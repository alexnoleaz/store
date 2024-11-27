using AutoMapper;
using Store.Shared.Dependency;

namespace Store.Shared;

/// <summary>
/// Implementation of <see cref="IObjectMapper"/> using AutoMapper.
/// </summary>
public class AutoMapperObjectMapper : IObjectMapper, ISingletonDependency
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoMapperObjectMapper"/> class.
    /// </summary>
    /// <param name="mapper">The AutoMapper <see cref="IMapper"/> instance to use.</param>
    public AutoMapperObjectMapper(IMapper mapper) => _mapper = mapper;

    /// <inheritdoc/>
    public TDestination Map<TDestination>(object source) => _mapper.Map<TDestination>(source);

    /// <inheritdoc/>
    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) =>
        _mapper.Map(source, destination);

    /// <inheritdoc/>
    public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source) =>
        _mapper.ProjectTo<TDestination>(source);
}