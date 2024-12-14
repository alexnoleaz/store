namespace Store.Shared.Services;

/// <summary>
/// Base class for domain services, providing common functionality like logging and object mapping.
/// </summary>
/// <typeparam name="TCategory">
/// The type used to define the category name for the logger. Typically, this is the type of the derived service class.
/// </typeparam>
public abstract class DomainService<TCategory> : ServiceBase<TCategory>, IDomainService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainService{TCategory}"/> class.
    /// </summary>
    /// <param name="logger">The logger for the <typeparamref name="TCategory"/> type.</param>
    /// <param name="objectMapper">The object mapper used within the service.</param>
    protected DomainService(ILogger<TCategory> logger, IObjectMapper objectMapper)
        : base(logger, objectMapper) { }
}
