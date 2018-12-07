using Couchbase.Core;

namespace BTI7252.DataAccess
{
	public interface ICouchBucketManager
	{
		IBucket Open(string name);
	}
}