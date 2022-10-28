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