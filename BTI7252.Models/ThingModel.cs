using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BTI7252.Models
{
	public class ThingModel
	{
		[Required]
		public Guid ThingId { get; set; }

		[Required]
		public string Description { get; set; }

		public DateTime? Created { get; set; }
		public DateTime? Updated { get; set; }

		[Required]
		public IEnumerable<ReadModel> Data { get; set; }

		[Required]
		public IEnumerable<EventModel> Events { get; set; }
	}
}