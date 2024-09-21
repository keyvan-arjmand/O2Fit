using Common;

namespace FoodStuff.Service.MessageBroker
{
    public interface IRabitMQProducer : IScopedDependency
    {
        public void SendProductMessage<T>(T message);
    }
}
