using System;
using System.Reactive.Linq;

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
}