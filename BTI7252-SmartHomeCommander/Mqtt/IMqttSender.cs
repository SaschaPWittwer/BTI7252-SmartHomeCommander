using System;
using System.Threading.Tasks;

namespace BTI7252_SmartHomeCommander.Mqtt
{
    public interface IMqttSender
    {
        Task SendMessage(string payload, Guid thingId, string eventName);
    }
}