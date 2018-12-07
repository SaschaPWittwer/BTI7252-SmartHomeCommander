using BTI7252_SmartHomeCommander.Mqtt;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BTI7252_SmartHomeCommander.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ThingsController : ControllerBase
    {
        private IMqttSender _mqttSender;

        public ThingsController(IMqttSender mqttSender)
        {
            _mqttSender = mqttSender;
        }

        [HttpPost("{thingId}/{eventName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> PostAsync(Guid thingId, string eventName, [FromBody] string payload)
        {
            try
            {
                await _mqttSender.SendMessage(payload, thingId, eventName);
            }
            catch (System.Exception ex)
            {
                // todo -> Log exception
                return StatusCode(500, ex.Message);
            }

            return Ok();
        }
    }
}