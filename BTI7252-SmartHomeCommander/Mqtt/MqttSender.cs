using System.Text;
using System.Threading.Tasks;
using BTI7252_SmartHomeCommander.Models;
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

        public async Task SendMessage(Command command)
        {
            await _conManager.EstablichConnection();

            var message = new MqttApplicationMessage
            {
                QualityOfServiceLevel = MqttQualityOfServiceLevel.ExactlyOnce,
                Payload = Encoding.UTF8.GetBytes(command.Payload),
                Topic = "" // todo -> From where?!
            };

            await _client.PublishAsync(message);
        }
    }
}