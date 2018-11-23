using System;
using System.Threading.Tasks;
using BTI7252_SmartHomeCommander.Models;

namespace BTI7252_SmartHomeCommander.Mqtt
{
    public interface IMqttSender
    {
        Task SendMessage(Command command, Guid thingId, string eventName);
    }
}