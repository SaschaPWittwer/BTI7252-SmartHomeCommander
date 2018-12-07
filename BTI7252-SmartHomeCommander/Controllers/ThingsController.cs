using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BTI7252.DataAccess;
using BTI7252.Models;
using BTI7252_SmartHomeCommander.Models.Things;
using Microsoft.AspNetCore.Mvc;

namespace BTI7252_SmartHomeCommander.Controllers
{
	[Route("/api/[controller]")]
	[ApiController]
	public class ThingsController : ControllerBase
	{
		private readonly ICouchRepository _couchRepositroy;

		public ThingsController(ICouchRepository couchRepositroy)
		{
			_couchRepositroy = couchRepositroy;
		}

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