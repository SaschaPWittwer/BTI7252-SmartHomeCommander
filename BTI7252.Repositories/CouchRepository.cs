using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BTI7252.Models;
using Couchbase;

namespace BTI7252.DataAccess
{
	public class CouchRepository : ICouchRepository
	{
		private readonly ICouchBucketManager _bucketManager;

		public CouchRepository(ICouchBucketManager bucketManager)
		{
			_bucketManager = bucketManager;
		}

		public async Task<ValidationResult> Save(ThingModel model)
		{
			using (var bucket = _bucketManager.Open("bucketboy"))
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
				{
					return new ValidationResult(documentResult.Message);
				}
			}

			return ValidationResult.Success;
		}
	}
}