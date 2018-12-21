using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BTI7252.Models;
using Couchbase;

namespace BTI7252.DataAccess
{
	public class CouchRepository : ICouchRepository
	{
		private readonly ICouchBucketManager _bucketManager;
		private readonly string _bucketname;

		public CouchRepository(ICouchBucketManager bucketManager, string bucketname)
		{
			_bucketManager = bucketManager;
			_bucketname = bucketname;
		}

		public async Task<ValidationResult> Save(ThingModel model)
		{
			using (var bucket = _bucketManager.Open(_bucketname))
			{
				var operationResult = bucket.Get<ThingModel>(model.ThingId.ToString());
				model.Created = operationResult?.Value?.Created ?? DateTime.UtcNow;
				model.Updated = DateTime.UtcNow;

				var document = new Document<ThingModel>
				{
					Id = model.ThingId.ToString(),
					Content = model
				};
				var documentResult = await bucket.UpsertAsync(document);
				if (!documentResult.Success)
					return new ValidationResult(documentResult.Message);
			}

			return ValidationResult.Success;
		}

		public IEnumerable<ThingModel> GetAll()
		{
			var ctx = _bucketManager.OpenContext(_bucketname);
			return ctx.Query<ThingModel>().ToList();
		}

		public ThingModel Get(Guid id)
		{
			using (var bucket = _bucketManager.Open(_bucketname))
			{
				return bucket.Get<ThingModel>(id.ToString()).Value;
			}
		}
	}
}