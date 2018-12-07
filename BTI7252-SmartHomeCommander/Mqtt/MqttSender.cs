using System;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

namespace BTI7252_SmartHomeCommander.Mqtt
{
    public class MqttSender : IMqttSender
    {
        private IMqttClient _client;
        private IMqttConnectionManager _conManager;

        public MqttSender(IMqttClient client, IMqttConnectionManager conManager)
        {
            _client = client;
            _conManager = conManager;
        }

        public async Task SendMessage(string payload, Guid thingId, string eventName)
        {
            await _conManager.EstablichConnection();

            var message = new MqttApplicationMessage
            {
                QualityOfServiceLevel = MqttQualityOfServiceLevel.AtLeastOnce,
                Payload = Encoding.UTF8.GetBytes(payload),
                Topic = $"nexhome/event/{thingId.ToString("D")}/{eventName}" 
            };

            await _client.PublishAsync(message);
        }
    }
}