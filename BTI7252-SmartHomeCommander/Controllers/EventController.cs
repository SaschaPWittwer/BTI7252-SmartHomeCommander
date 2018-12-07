using BTI7252_SmartHomeCommander.Models;
using BTI7252_SmartHomeCommander.Mqtt;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BTI7252_SmartHomeCommander.Controllers
{
    [Route("/nexhome/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IMqttSender _mqttSender;

        public EventController(IMqttSender mqttSender)
        {
            _mqttSender = mqttSender;
        }

        [HttpPost("{thingId}/{eventName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> PostAsync(string thingId, string eventName, [FromBody] Command command)
        {
            
            if (!command.IsValid())
                return Ok($"{thingId} {eventName}");

            try
            {
                await _mqttSender.SendMessage(command, new Guid(thingId), eventName);
            }
            catch (System.Exception ex)
            {
                // todo -> Log exception
                return StatusCode(500, ex.Message);
            }

            return Ok($"{thingId} {eventName}");
        }
    }
}