using Store.Shared.Dependency;

namespace Store.Shared.Services;

/// <summary>
/// Interface that marks a service as a domain service in the application.
/// It is typically used to define business logic operations that are part of the domain layer.
/// </summary>
public interface IDomainService : ITransientDependency { }
