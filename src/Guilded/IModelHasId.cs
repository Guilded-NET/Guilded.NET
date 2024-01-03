namespace Guilded;

/// <summary>
/// Represents a model that has an <see cref="Id">identifier</see>.
/// </summary>
/// <typeparam name="T">The type of the <see cref="Id">identifier</see></typeparam>
public interface IModelHasId<out T> where T : notnull
{
    /// <summary>
    /// Gets the identifier of the <see cref="IModelHasId{T}">content</see>.
    /// </summary>
    /// <value>The identifier of the <see cref="IModelHasId{T}">content</see></value>
    T Id { get; }
}

/// <summary>
/// Represents a model that has a <see cref="Name">name</see>.
/// </summary>
public interface IModelHasName
{
    /// <summary>
    /// Gets the name of the <see cref="IModelHasId{T}">content</see>.
    /// </summary>
    /// <value>The name of the <see cref="IModelHasId{T}">content</see></value>
    string Name { get; }
}