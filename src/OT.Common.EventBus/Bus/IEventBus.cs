using System.Threading.Tasks;
using OT.Common.EventBus.Commands;
using OT.Common.EventBus.Events;

namespace OT.Common.EventBus.Bus
{
    public interface IEventBus
    {
        Task sendCommand<T>(T command) where T : Command;
        void Publish<T>(T @event) where T : Event;

        void Subcribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>;
    }
}
