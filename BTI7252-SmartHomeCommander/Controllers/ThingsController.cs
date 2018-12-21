using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BTI7252.DataAccess;
using BTI7252.Models;
using BTI7252_SmartHomeCommander.Mqtt;
using Microsoft.AspNetCore.Mvc;

namespace BTI7252_SmartHomeCommander.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	public class ThingsController : ControllerBase
	{
		private readonly ICouchRepository _couchRepositroy;
		private readonly IMqttSender _mqttSender;

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
			catch (Exception ex)
			{
				// todo -> Log exception
				return StatusCode(500, ex.Message);
			}

			return Ok();
		}

		[HttpGet]
		public IEnumerable<ThingModel> GetAll()
		{
			return _couchRepositroy.GetAll();
		}

		[HttpPost]
		[Route("Register")]
		public async Task<ActionResult> Register(ThingModel model)
		{
			if (model == null)
				return BadRequest();

			var result = await _couchRepositroy.Save(model);
			if (result != null && !string.IsNullOrEmpty(result.ErrorMessage))
				return BadRequest(result.ErrorMessage);

			return Ok(model);
		}

		[HttpGet]
		[Route("easteregg")]
		public async Task<ActionResult> Get()
		{
			var result = "B========D";
			return Ok(result);
		}
	}
}