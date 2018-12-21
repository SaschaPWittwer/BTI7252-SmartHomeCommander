using System;
using Couchbase.Core;
using Couchbase.Linq;

namespace BTI7252.DataAccess
{
	public interface ICouchBucketManager
	{
		IBucket Open(string name);
		IBucketContext OpenContext(string name);
	}
}