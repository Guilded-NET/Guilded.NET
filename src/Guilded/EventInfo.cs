using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Guilded.Base.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Guilded;

/// <summary>
/// Represents the base interface for event info.
/// </summary>
/// <typeparam name="T">The type of the observable</typeparam>
/// <seealso cref="EventInfo{T}" />
/// <seealso cref="AbstractGuildedClient" />
public interface IEventInfo<out T>
{
    #region Properties
    /// <summary>
    /// An observable that can be subscribed.
    /// </summary>
    /// <remarks>
    /// <para>An observable that can be subscribed to. The received event will be of type <typeparamref name="T" />.</para>
    /// </remarks>
    /// <value>Observable</value>
    IObservable<T> Observable { get; }

    /// <summary>
    /// The type of the arguments that should be used.
    /// </summary>
    /// <remarks>
    /// <para>The type of the event that will be received. Relies on <typeparamref name="T" /> type.</para>
    /// </remarks>
    /// <value>Type</value>
    Type ArgumentType { get; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    Func<Type, JsonSerializer, GuildedSocketMessage, object> Transform { get; }
    #endregion

    #region Methods
    /// <summary>
    /// Notifies observers with OnNext.
    /// </summary>
    /// <remarks>
    /// <para>Notifies all <see cref="Observable" />'s observers.</para>
    /// </remarks>
    /// <param name="value">The next received value</param>
    void OnNext(object value);

    /// <summary>
    /// Notifies observers with OnError.
    /// </summary>
    /// <remarks>
    /// <para>Notifies all <see cref="Observable" />'s observers with an error.</para>
    /// </remarks>
    /// <param name="exception">The next received exception/error</param>
    void OnError(Exception exception);
    #endregion
}
/// <summary>
/// efines a new Guilded event that can be used in <see cref="AbstractGuildedClient.GuildedEvents" />
/// </summary>
/// <remarks>
/// <para>The event can be subscribed via <see cref="Observable" />.</para>
/// </remarks>
/// <typeparam name="T">The type of the event that will be received. Used in <see cref="Subject" /> and <see cref="Observable" /></typeparam>
/// <seealso cref="IEventInfo{T}" />
/// <seealso cref="AbstractGuildedClient" />
public class EventInfo<T> : IEventInfo<T>
{
    #region Static fields
    private static readonly Func<Type, JsonSerializer, GuildedSocketMessage, object> DefaultTransform = (type, serializer, message) => message.RawData!.ToObject(type, serializer)!;
    #endregion

    #region Fields
    /// <summary>
    /// The subject that will be used for subscribing to this event.
    /// </summary>
    /// <returns>Subject</returns>
    protected internal Subject<T> Subject { get; } = new();
    #endregion

    #region Properties
    /// <summary>
    /// Gets the observable event that can be used to subscribe to the event.
    /// </summary>
    /// <value><see cref="Subject" /> as observable</value>
    public IObservable<T> Observable => Subject.AsObservable();

    /// <inheritdoc />
    public Type ArgumentType => typeof(T);

    /// <inheritdoc />
    public Func<Type, JsonSerializer, GuildedSocketMessage, object> Transform { get; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of <see cref="EventInfo{T}" />.
    /// </summary>
    /// <param name="transform"></param>
    public EventInfo(Func<Type, JsonSerializer, GuildedSocketMessage, object> transform) =>
        Transform = transform;

    /// <summary>
    /// Initializes a new instance of <see cref="EventInfo{T}" />.
    /// </summary>
    public EventInfo() : this(DefaultTransform) { }
    #endregion

    #region Methods
    /// <summary>
    /// Notifies the observers with new value.
    /// </summary>
    /// <param name="value">The next received value</param>
    public void OnNext(object value) =>
        Subject.OnNext((T)value);

    /// <summary>
    /// Notifies observers with an exception.
    /// </summary>
    /// <param name="exception">The next received exception/error</param>
    public void OnError(Exception exception) =>
        Subject.OnError(exception);
    #endregion
}