using BTI7252_SmartHomeCommander.Models;
using BTI7252_SmartHomeCommander.Mqtt;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BTI7252_SmartHomeCommander.Controllers
{
    [Route("/nexhome/[controller]/{thingId}/{eventName}")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IMqttSender _mqttSender;

        public EventController(IMqttSender mqttSender)
        {
            _mqttSender = mqttSender;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> PostAsync([FromBody] Command command, Guid thingId, string eventName)
        {
            if (!command.IsValid())
                return BadRequest();

            try
            {
                await _mqttSender.SendMessage(command, thingId, eventName);
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