using System.Threading.Tasks;
using OT.Common.EventBus.Events;

namespace OT.Common.EventBus.Bus
{
    public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent:Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHandler
    {
    }
}
