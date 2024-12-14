using Store.Shared.Dependency;

namespace Store.Shared.Services;

/// <summary>
/// Interface to be implemented by all domain services for identification by convention.
/// </summary>
public interface IDomainService : ITransientDependency { }
