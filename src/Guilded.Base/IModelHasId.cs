namespace Guilded.Base;

/// <summary>
/// Represents a model that has <see cref="Id">an identifier</see>.
/// </summary>
/// <typeparam name="T">The type of <see cref="Id">the identifier</see></typeparam>
public interface IModelHasId<out T>
{
    /// <summary>
    /// Gets the identifier of <see cref="IModelHasId{T}">the content</see>.
    /// </summary>
    /// <value><see cref="IModelHasId{T}.Id">Content ID</see></value>
    T Id { get; }
}