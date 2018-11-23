using System.Threading.Tasks;
using BTI7252_SmartHomeCommander.Models;
using BTI7252_SmartHomeCommander.Models.Things;
using BTI7252_SmartHomeCommander.Mqtt;
using Microsoft.AspNetCore.Mvc;
using MQTTnet.Client;

namespace BTI7252_SmartHomeCommander.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ThingsController : ControllerBase
    {

        /// <summary>
        ///     Registers a new sensors metadata
        /// </summary>
        /// <param name="data">the sensor meta data</param>
        /// <returns><code>Ok</code> if the sensor could be registered successfully, <code>Failure</code> otherwise</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> PostAsync(ThingMetadata data)
        {
            return Ok();
        }
    }
}