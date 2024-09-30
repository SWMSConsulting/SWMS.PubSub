using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMS.PubSub
{
    public record EventMetadata(string CorrelationId);
    public record Event<T>(T? Data, EventMetadata? Metadata = default);

    public interface IEventHandler<in T>
    {
        ValueTask Handle(T? time, CancellationToken token = default);
    }
    public interface IConsumer : IAsyncDisposable
    {
        ValueTask Start(CancellationToken token = default);

        ValueTask Stop(CancellationToken token = default);
    }

    public interface IConsumer<T> : IConsumer
    {
    }

    public interface IProducer<T> : IAsyncDisposable
    {
        ValueTask Publish(Event<T> @event, CancellationToken token = default);
    }

    public interface IEventContextAccessor<T>
    {
        public Event<T>? Event { get; }

        void Set(Event<T> @event);
    }
}
