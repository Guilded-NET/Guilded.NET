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
        /// A subject as an observable.
        /// </summary>
        /// <value>Subject</value>
        IObservable<T> Observable { get; }
        /// <summary>
        /// The type of the arguments that should be used.
        /// </summary>
        /// <value>Type</value>
        Type ArgumentType { get; set; }
        /// <summary>
        /// Invokes <see cref="Subject"/>'s OnNext method.
        /// </summary>
        /// <param name="value">The next received value</param>
        void OnNext(object value);
    }
    /// <summary>
    /// An information about a Guilded.NET event.
    /// </summary>
    public class EventInfo<T> : IEventInfo<T>
    {
        /// <summary>
        /// A subject that will be used as an asynchronous event.
        /// </summary>
        /// <returns>Subject</returns>
        protected internal Subject<T> Subject = new Subject<T>();
        /// <summary>
        /// A subject as an observable.
        /// </summary>
        /// <value>Subject</value>
        public IObservable<T> Observable => Subject.AsObservable();
        /// <summary>
        /// The type of the arguments that should be used.
        /// </summary>
        /// <value>Type</value>
        public Type ArgumentType
        {
            get; set;
        }
        /// <summary>
        /// An information about a Guilded.NET event.
        /// </summary>
        /// <param name="argumentType">The type of the arguments that should be used</param>
        public EventInfo(Type argumentType) =>
            ArgumentType = argumentType;
        /// <summary>
        /// Invokes <see cref="Subject"/>'s OnNext method.
        /// </summary>
        /// <param name="value">The next received value</param>
        public void OnNext(object value) =>
            Subject.OnNext((T)value);
    }
}