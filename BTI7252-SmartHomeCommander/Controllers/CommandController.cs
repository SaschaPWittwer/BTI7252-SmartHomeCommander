using System.Threading.Tasks;
using BTI7252_SmartHomeCommander.Models;
using BTI7252_SmartHomeCommander.Mqtt;
using Microsoft.AspNetCore.Mvc;
using MQTTnet.Client;

namespace BTI7252_SmartHomeCommander.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private IMqttSender _mqttSender;

        public CommandController(IMqttSender mqttSender)
        {
            _mqttSender = mqttSender;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> PostAsync(Command command)
        {
            if (!command.IsValid())
                return BadRequest();

            try
            {
                await _mqttSender.SendMessage(command);
            }
            catch (System.Exception ex)
            {
                // todo -> Log exception
                return StatusCode(500);
            }

            return Ok();
        }


    }
}