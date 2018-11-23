using System.Threading.Tasks;

namespace BTI7252_SmartHomeCommander.Mqtt
{
    public interface IMqttConnectionManager
    {
        Task EstablichConnection();
    }
}