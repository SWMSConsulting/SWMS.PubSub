using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMS.PubSub
{
    internal sealed class EventContextAccessor<T> : IEventContextAccessor<T>
    {
        private static readonly AsyncLocal<EventMetadataWrapper<T>> Holder = new();

        public Event<T>? Event => Holder.Value?.Event;

        public void Set(Event<T> @event)
        {
            var holder = Holder.Value;
            if (holder != null)
            {
                holder.Event = null;
            }

            Holder.Value = new EventMetadataWrapper<T> { Event = @event };
        }
    }

    internal sealed class EventMetadataWrapper<T>
    {
        public Event<T>? Event { get; set; }
    }
}
