using System.Threading.Tasks;
using MQTTnet.Client;
using MQTTnet.Exceptions;

namespace BTI7252_SmartHomeCommander.Mqtt
{
    public class MqttConnectionManager : IMqttConnectionManager
    {
        private IMqttClient _client;
        private IMqttClientOptions _options;

        public MqttConnectionManager(IMqttClient client, IMqttClientOptions options)
        {
            _client = client;
            _options = options;
        }

        public async Task EstablichConnection()
        {
            if (_client.IsConnected)
                return;

            var conResult = await _client.ConnectAsync(_options);
            if (!conResult.IsSessionPresent)
                throw new MqttCommunicationException("Could not establish connection to mqtt broker");
        }
    }
}