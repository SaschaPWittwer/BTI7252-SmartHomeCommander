using System;
using System.Collections.Generic;

namespace BTI7252.Models
{
	public class ThingModel
	{
		public Guid ThingId { get; set; }
		public string Descriptions { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }

		public IEnumerable<ReadModel> Data { get; set; }
		public IEnumerable<EventModel> Events { get; set; }
	}
}