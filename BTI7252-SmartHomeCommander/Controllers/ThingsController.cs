using System.Threading.Tasks;
using BTI7252.Models;
using BTI7252_SmartHomeCommander.Models.Things;
using Microsoft.AspNetCore.Mvc;

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

		[HttpPost]
		[Route("Register")]
		public async Task<ActionResult> Register([FromBody]ThingModel model)
		{
			if (model == null)
			{
				return BadRequest();
			}

			return Ok();
		}

		[HttpGet]
		public async Task<ActionResult> Get()
		{
			var result = "B========D";
			return Ok(result);
		}
	}
}