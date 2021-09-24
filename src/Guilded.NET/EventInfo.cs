using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Guilded.NET
{
    /// <summary>
    /// The base interface for event info.
    /// </summary>
    /// <typeparam name="T">The type of the observable</typeparam>
    public interface IEventInfo<out T>
    {
        /// <summary>
        /// A subscribable observable.
        /// </summary>
        /// <remarks>
        /// <para>An observable that can be subscribed to. The received event will be of type <typeparamref name="T"/>.</para>
        /// </remarks>
        /// <value>Observable</value>
        IObservable<T> Observable { get; }
        /// <summary>
        /// The type of the arguments that should be used.
        /// </summary>
        /// <remarks>
        /// <para>The type of the event that will be received. Relies on <typeparamref name="T"/> type.</para>
        /// </remarks>
        /// <value>Type</value>
        Type ArgumentType { get; }
        /// <summary>
        /// Notifies observers with OnNext.
        /// </summary>
        /// <remarks>
        /// <para>Notifies all <see cref="Observable"/>'s observers.</para>
        /// </remarks>
        /// <param name="value">The next received value</param>
        void OnNext(object value);
    }
    /// <summary>
    /// Defines a Guilded event.
    /// </summary>
    /// <remarks>
    /// <para>Defines a new Guilded event that can be used in <see cref="AbstractGuildedClient.GuildedEvents"/></para>
    /// <para>The event can be subscribed via <see cref="Observable"/>.</para>
    /// </remarks>
    /// <typeparam name="T">The type of the event that will be received. Used in <see cref="Subject"/> and <see cref="Observable"/></typeparam>
    public class EventInfo<T> : IEventInfo<T>
    {
        /// <summary>
        /// A subject that will be used as an observable.
        /// </summary>
        /// <remarks>
        /// <para>Subject that will be used as an observable in <see cref="Observable"/>.</para>
        /// </remarks>
        /// <returns>Subject</returns>
        protected internal Subject<T> Subject = new Subject<T>();
        /// <summary>
        /// A subscribable observable.
        /// </summary>
        /// <remarks>
        /// <para>An observable that can be subscribed to. The received event will be of type <typeparamref name="T"/>.</para>
        /// <para>This relies on <see cref="Subject"/>.</para>
        /// </remarks>
        /// <value><see cref="Subject"/> as observable</value>
        public IObservable<T> Observable => Subject.AsObservable();
        /// <summary>
        /// The type of the arguments that should be used.
        /// </summary>
        /// <remarks>
        /// <para>The type of the event that will be received. Relies on <typeparamref name="T"/> type.</para>
        /// </remarks>
        /// <value>Type</value>
        public Type ArgumentType => typeof(T);
        /// <summary>
        /// Creates a new Guilded event.
        /// </summary>
        public EventInfo() {}
        /// <summary>
        /// Notifies observers with OnNext.
        /// </summary>
        /// <remarks>
        /// <para>Notifies all <see cref="Observable"/>'s observers. Invokes <see cref="Subject"/>'s <see cref="Subject{T}.OnNext(T)"/> method.</para>
        /// </remarks>
        /// <param name="value">The next received value</param>
        public void OnNext(object value) =>
            Subject.OnNext((T)value);
        /// <summary>
        /// Notifies observers with OnError.
        /// </summary>
        /// <remarks>
        /// <para>Notifies all <see cref="Observable"/>'s observers with an error. Invokes <see cref="Subject"/>'s <see cref="Subject{T}.OnError(Exception)"/> method.</para>
        /// </remarks>
        /// <param name="exception">The next received exception/error</param>
        public void OnError(Exception exception) =>
            Subject.OnError(exception);
    }
}