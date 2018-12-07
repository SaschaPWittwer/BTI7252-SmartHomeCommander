using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BTI7252.DataAccess;
using BTI7252.Models;
using BTI7252_SmartHomeCommander.Models.Things;
using Microsoft.AspNetCore.Mvc;
using BTI7252_SmartHomeCommander.Mqtt;

namespace BTI7252_SmartHomeCommander.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	public class ThingsController : ControllerBase
	{
    private IMqttSender _mqttSender;
		private readonly ICouchRepository _couchRepositroy;

		public ThingsController(ICouchRepository couchRepositroy, IMqttSender mqttSender)
		{
			_couchRepositroy = couchRepositroy;
      _mqttSender = mqttSender;
		}
    
    [HttpPut("{thingId}/{eventName}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> PutAsync(Guid thingId, string eventName, [FromBody] string payload)
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

		[HttpPost]
		[Route("Register")]
		public async Task<ActionResult> Register(ThingModel model)
		{
			if (model == null)
			{
				return BadRequest();
			}

			ValidationResult result = await _couchRepositroy.Save(model);
			if (result != null && !string.IsNullOrEmpty(result.ErrorMessage))
			{
				return BadRequest(result.ErrorMessage);
			}

			return Ok(model);
		}

		[HttpGet]
		public async Task<ActionResult> Get()
		{
			var result = "B========D";
			return Ok(result);
		}
	}
}
