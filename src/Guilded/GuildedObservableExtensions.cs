using System;
using System.Reactive.Linq;
using Guilded.Base;
using Guilded.Content;
using Guilded.Servers;
using Guilded.Users;

namespace Guilded;

/// <summary>
/// Adds Guilded and non-Guilded related extensions to <see cref="IObservable{T}">observables</see>.
/// </summary>
public static class GuildedObservableExtensions
{
    /// <summary>
    /// Returns elements from an observable sequence until specific time.
    /// </summary>
    /// <param name="observable">The observable sequence to filter</param>
    /// <param name="absoluteElapse">The absolute date and time to expire on</param>
    /// <typeparam name="T">The type of the observable sequence element</typeparam>
    /// <returns>Elements from an observable sequence until specific time</returns>
    /// <seealso cref="ElapseOnIfNothing{T}(IObservable{T}, TimeSpan)" />
    public static IObservable<T> ElapseOn<T>(this IObservable<T> observable, DateTime absoluteElapse) =>
        observable.TakeWhile(_ => DateTime.Now < absoluteElapse);

    /// <summary>
    /// Returns elements from an observable sequence until specific time.
    /// </summary>
    /// <param name="observable">The observable sequence to filter</param>
    /// <param name="timeSpan">The absolute expiration time span</param>
    /// <typeparam name="T">The type of the observable sequence element</typeparam>
    /// <returns>Elements from an observable sequence until specific time</returns>
    /// <seealso cref="ElapseOnIfNothing{T}(IObservable{T}, TimeSpan)" />
    public static IObservable<T> ElapseOn<T>(this IObservable<T> observable, TimeSpan timeSpan) =>
        ElapseOn(observable, DateTime.Now + timeSpan);

    /// <summary>
    /// Returns elements from an observable sequence until there are no given items for specific period of time.
    /// </summary>
    /// <param name="observable">The observable sequence to filter</param>
    /// <param name="timeSpan">The sliding expiration time span</param>
    /// <typeparam name="T">The type of the observable sequence element</typeparam>
    /// <returns>Elements from an observable sequence until there are no given items for specific period of time</returns>
    /// <seealso cref="ElapseOn{T}(IObservable{T}, TimeSpan)" />
    public static IObservable<T> ElapseOnIfNothing<T>(this IObservable<T> observable, TimeSpan timeSpan)
    {
        DateTime slidingElapse = DateTime.Now + timeSpan;

        return observable.TakeWhile(_ =>
        {
            if (DateTime.Now < slidingElapse)
            {
                // Could do +=, but then one could spam until the time span becomes 10 years
                slidingElapse = DateTime.Now + timeSpan;
                return true;
            }

            return false;
        });
    }

    /// <summary>
    /// Filters elements of an <see cref="IObservable{T}">observable</see> sequence to only <see cref="IModelHasId{T}">Guilded elements</see> with the <paramref name="id">specified identifier</paramref>.
    /// </summary>
    /// <param name="observable">The <see cref="IObservable{T}">observable</see> sequence to filter out</param>
    /// <param name="id">The identifier of the <see cref="IModelHasId{T}">Guilded element</see> to filter based on</param>
    /// <typeparam name="TElement">The type of the <see cref="IModelHasId{T}">Guilded model elements</see></typeparam>
    /// <typeparam name="TId">The type of the <paramref name="id">specified identifier</paramref> of the <see cref="IModelHasId{T}">Guilded element</see></typeparam>
    /// <returns>Filtered <see cref="IObservable{T}">observable</see> sequence</returns>
    /// <seealso cref="InServer{T}(IObservable{T}, HashId)" />
    /// <seealso cref="InServer{T}(IObservable{T}, Server)" />
    /// <seealso cref="InServer{T}(IObservable{T}, IServerBased)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, Guid)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, IChannel)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, IChannelBased)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, HashId)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, IUser)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, ICreatableContent)" />
    public static IObservable<TElement> HasId<TElement, TId>(this IObservable<TElement> observable, TId id) where TElement : IModelHasId<TId> where TId : notnull =>
        observable.Where(x => x.Id.Equals(id));

    /// <summary>
    /// Filters elements of an <see cref="IObservable{T}">observable</see> sequence to only elements from the <paramref name="server">specified server</paramref>.
    /// </summary>
    /// <param name="observable">The <see cref="IObservable{T}">observable</see> sequence to filter out</param>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the elements need to be in</param>
    /// <typeparam name="T">The type of the <see cref="IModelHasId{T}">Guilded elements</see></typeparam>
    /// <returns>Filtered <see cref="IObservable{T}">observable</see> sequence</returns>
    /// <seealso cref="HasId{TModel, TId}(IObservable{TModel}, TId)" />
    /// <seealso cref="InServer{T}(IObservable{T}, IServerBased)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, Guid)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, IChannel)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, IChannelBased)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, HashId)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, IUser)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, ICreatableContent)" />
    public static IObservable<T> InServer<T>(this IObservable<T> observable, HashId server) where T : IServerBased =>
        observable.Where(x => server.Equals(x.ServerId));

    /// <inheritdoc cref="InServer{T}(IObservable{T}, HashId)" />
    /// <param name="observable">The <see cref="IObservable{T}">observable</see> sequence to filter out</param>
    /// <param name="server">The identifier of the <see cref="Server">server</see> where the elements need to be in</param>
    /// <typeparam name="T">The type of the <see cref="IModelHasId{T}">Guilded elements</see></typeparam>
    public static IObservable<T> InServer<T>(this IObservable<T> observable, Server server) where T : IServerBased =>
        InServer(observable, server.Id);

    /// <summary>
    /// Filters elements of an <see cref="IObservable{T}">observable</see> sequence to only elements from the <see cref="Server">server</see> of the <paramref name="content">specified content</paramref>.
    /// </summary>
    /// <param name="observable">The <see cref="IObservable{T}">observable</see> sequence to filter out</param>
    /// <param name="content">The content from <see cref="Server">server</see> where the elements need to be in</param>
    /// <typeparam name="T">The type of the <see cref="IModelHasId{T}">Guilded elements</see></typeparam>
    /// <returns>Filtered <see cref="IObservable{T}">observable</see> sequence</returns>
    /// <seealso cref="HasId{TModel, TId}(IObservable{TModel}, TId)" />
    /// <seealso cref="InServer{T}(IObservable{T}, HashId)" />
    /// <seealso cref="InServer{T}(IObservable{T}, Server)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, Guid)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, IChannel)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, IChannelBased)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, HashId)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, IUser)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, ICreatableContent)" />
    public static IObservable<T> InServer<T>(this IObservable<T> observable, IServerBased content) where T : IServerBased =>
        InServer(observable, content.ServerId);

    /// <summary>
    /// Filters elements of an <see cref="IObservable{T}">observable</see> sequence to only elements from the <paramref name="channel">specified channel</paramref>.
    /// </summary>
    /// <param name="observable">The <see cref="IObservable{T}">observable</see> sequence to filter out</param>
    /// <param name="channel">The identifier of the <see cref="IChannel">channel</see> where the elements need to be in</param>
    /// <typeparam name="T">The type of the <see cref="IModelHasId{T}">Guilded elements</see></typeparam>
    /// <returns>Filtered <see cref="IObservable{T}">observable</see> sequence</returns>
    /// <seealso cref="HasId{TModel, TId}(IObservable{TModel}, TId)" />
    /// <seealso cref="InServer{T}(IObservable{T}, HashId)" />
    /// <seealso cref="InServer{T}(IObservable{T}, Server)" />
    /// <seealso cref="InServer{T}(IObservable{T}, IServerBased)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, IChannelBased)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, HashId)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, IUser)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, ICreatableContent)" />
    public static IObservable<T> InChannel<T>(this IObservable<T> observable, Guid channel) where T : IChannelBased =>
        observable.Where(x => x.ChannelId == channel);

    /// <inheritdoc cref="InChannel{T}(IObservable{T}, Guid)" />
    /// <param name="observable">The <see cref="IObservable{T}">observable</see> sequence to filter out</param>
    /// <param name="channel">The identifier of the <see cref="IChannel">channel</see> where the elements need to be in</param>
    /// <typeparam name="T">The type of the <see cref="IModelHasId{T}">Guilded elements</see></typeparam>
    public static IObservable<T> InChannel<T>(this IObservable<T> observable, IChannel channel) where T : IChannelBased =>
        InChannel(observable, channel.Id);

    /// <summary>
    /// Filters elements of an <see cref="IObservable{T}">observable</see> sequence to only elements from the <see cref="IChannel">channel</see> of the <paramref name="content">specified content</paramref>.
    /// </summary>
    /// <param name="observable">The <see cref="IObservable{T}">observable</see> sequence to filter out</param>
    /// <param name="content">The content from <see cref="Server">server</see> where the elements need to be in</param>
    /// <typeparam name="T">The type of the <see cref="IModelHasId{T}">Guilded elements</see></typeparam>
    /// <returns>Filtered <see cref="IObservable{T}">observable</see> sequence</returns>
    /// <seealso cref="HasId{TModel, TId}(IObservable{TModel}, TId)" />
    /// <seealso cref="InServer{T}(IObservable{T}, HashId)" />
    /// <seealso cref="InServer{T}(IObservable{T}, Server)" />
    /// <seealso cref="InServer{T}(IObservable{T}, IServerBased)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, Guid)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, IChannel)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, HashId)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, IUser)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, ICreatableContent)" />
    public static IObservable<T> InChannel<T>(this IObservable<T> observable, IChannelBased content) where T : IChannelBased =>
        InChannel(observable, content.ChannelId);

    /// <summary>
    /// Filters elements of an <see cref="IObservable{T}">observable</see> sequence to only elements created by the <paramref name="user">specified user</paramref>.
    /// </summary>
    /// <param name="observable">The <see cref="IObservable{T}">observable</see> sequence to filter out</param>
    /// <param name="user">The identifier of the <see cref="User">user</see> who needs to be the author of the elements</param>
    /// <typeparam name="T">The type of the <see cref="IModelHasId{T}">Guilded elements</see></typeparam>
    /// <returns>Filtered <see cref="IObservable{T}">observable</see> sequence</returns>
    /// <seealso cref="HasId{TModel, TId}(IObservable{TModel}, TId)" />
    /// <seealso cref="InServer{T}(IObservable{T}, HashId)" />
    /// <seealso cref="InServer{T}(IObservable{T}, Server)" />
    /// <seealso cref="InServer{T}(IObservable{T}, IServerBased)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, Guid)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, IChannel)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, IChannelBased)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, ICreatableContent)" />
    public static IObservable<T> CreatedBy<T>(this IObservable<T> observable, HashId user) where T : ICreatableContent =>
        observable.Where(x => x.CreatedBy == user);

    /// <inheritdoc cref="CreatedBy{T}(IObservable{T}, HashId)" />
    /// <param name="observable">The <see cref="IObservable{T}">observable</see> sequence to filter out</param>
    /// <param name="user">The <see cref="User">user</see> who needs to be the author of the elements</param>
    /// <typeparam name="T">The type of the <see cref="IModelHasId{T}">Guilded elements</see></typeparam>
    public static IObservable<T> CreatedBy<T>(this IObservable<T> observable, IUser user) where T : ICreatableContent =>
        CreatedBy(observable, user.Id);

    /// <summary>
    /// Filters elements of an <see cref="IObservable{T}">observable</see> sequence to only elements created by the <see cref="User">user</see> of the <paramref name="content">specified content</paramref>.
    /// </summary>
    /// <param name="observable">The <see cref="IObservable{T}">observable</see> sequence to filter out</param>
    /// <param name="content">The content from the content <see cref="User">author</see> by who the elements need to be created</param>
    /// <typeparam name="T">The type of the <see cref="IModelHasId{T}">Guilded elements</see></typeparam>
    /// <returns>Filtered <see cref="IObservable{T}">observable</see> sequence</returns>
    /// <seealso cref="HasId{TModel, TId}(IObservable{TModel}, TId)" />
    /// <seealso cref="InServer{T}(IObservable{T}, HashId)" />
    /// <seealso cref="InServer{T}(IObservable{T}, Server)" />
    /// <seealso cref="InServer{T}(IObservable{T}, IServerBased)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, Guid)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, IChannel)" />
    /// <seealso cref="InChannel{T}(IObservable{T}, IChannelBased)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, HashId)" />
    /// <seealso cref="CreatedBy{T}(IObservable{T}, IUser)" />
    public static IObservable<T> CreatedBy<T>(this IObservable<T> observable, ICreatableContent content) where T : ICreatableContent =>
        CreatedBy(observable, content.CreatedBy);
}