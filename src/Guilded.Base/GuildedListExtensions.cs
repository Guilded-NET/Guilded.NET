using System.Collections.Generic;
using System.Linq;

namespace Guilded.Base;

/// <summary>
/// Adds extensions to <see cref="IList{T}">lists</see> with <see cref="IModelHasId{T}">Guilded models with IDs</see>.
/// </summary>
/// <seealso cref="IModelHasId{T}" />
/// <seealso cref="" />
/// <seealso cref="ContentModel" />
public static class GuildedListExtensions
{
    /// <summary>
    /// Returns an <typeparamref name="TModel">element</typeparamref> in the list that has the specified <paramref name="id">identifier</paramref>.
    /// </summary>
    /// <param name="list">The list to get <typeparamref name="TModel">element</typeparamref> from</param>
    /// <param name="id">The <see cref="IModelHasId{T}.Id">identifier</see> of the <typeparamref name="TModel">element</typeparamref> being searched for</param>
    /// <typeparam name="TModel">The type of the <typeparamref name="TModel">element</typeparamref> that is being fetched</typeparam>
    /// <typeparam name="TId">The type of the <see cref="IModelHasId{T}.Id">identifier</see> of the <typeparamref name="TModel">element</typeparamref></typeparam>
    /// <returns>An <typeparamref name="TModel">element</typeparamref> in the list that has the specified <paramref name="id">identifier</paramref></returns>
    public static TModel ById<TModel, TId>(this IList<TModel> list, TId id) where TModel : IModelHasId<TId> =>
        list.First(x => x.Id!.Equals(id));

    /// <summary>
    /// Returns an <typeparamref name="TModel">element</typeparamref> in the list that has the specified <paramref name="id">identifier</paramref> or default if no such element exists in the list.
    /// </summary>
    /// <param name="list">The list to get <typeparamref name="TModel">element</typeparamref> from</param>
    /// <param name="id">The <see cref="IModelHasId{T}.Id">identifier</see> of the <typeparamref name="TModel">element</typeparamref> being searched for</param>
    /// <typeparam name="TModel">The type of the <typeparamref name="TModel">element</typeparamref> that is being fetched</typeparam>
    /// <typeparam name="TId">The type of the <see cref="IModelHasId{T}.Id">identifier</see> of the <typeparamref name="TModel">element</typeparamref></typeparam>
    /// <returns>An <typeparamref name="TModel">element</typeparamref> in the list that has the specified <paramref name="id">identifier</paramref> or <see langword="default" /> of <typeparamref name="TModel" /> if no such element exists in the list</returns>
    public static TModel? ByIdOrDefault<TModel, TId>(this IList<TModel> list, TId id) where TModel : IModelHasId<TId> =>
        list.FirstOrDefault(x => x.Id!.Equals(id));

    /// <summary>
    /// Returns whether an <typeparamref name="TModel">element</typeparamref> in the list that has the specified <paramref name="id">identifier</paramref>.
    /// </summary>
    /// <param name="list">The list to get <typeparamref name="TModel">element</typeparamref> check</param>
    /// <param name="id">The <see cref="IModelHasId{T}.Id">identifier</see> of the <typeparamref name="TModel">element</typeparamref> being searched for</param>
    /// <typeparam name="TModel">The type of the <typeparamref name="TModel">element</typeparamref> that is being fetched</typeparam>
    /// <typeparam name="TId">The type of the <see cref="IModelHasId{T}.Id">identifier</see> of the <typeparamref name="TModel">element</typeparamref></typeparam>
    /// <returns>Whether the <paramref name="list" /> contains an <typeparamref name="TModel">element</typeparamref> with the specified <paramref name="id">identifier</paramref></returns>
    public static bool Contains<TModel, TId>(this IList<TModel> list, TId id) where TModel : IModelHasId<TId> =>
        list.Any(x => x.Id!.Equals(id));
}