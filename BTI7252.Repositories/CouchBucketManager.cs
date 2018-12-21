using System;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Couchbase.Linq;

namespace BTI7252.DataAccess
{
	public class CouchBucketManager : ICouchBucketManager
	{
		private readonly ICouchConnectionManager _connectionManager;

		public CouchBucketManager(ICouchConnectionManager connectionManager)
		{
			_connectionManager = connectionManager;
		}

		public IBucket Open(string name)
		{
			var cluster = new Cluster(new ClientConfiguration
			{
				Servers = new List<Uri> {_connectionManager.ApiUrl}
			});

			var authenticator = new PasswordAuthenticator(_connectionManager.Username, _connectionManager.Password);
			cluster.Authenticate(authenticator);
			var bucket = cluster.OpenBucket(name);
			return bucket;
		}

		public IBucketContext OpenContext(string name)
		{
			return new BucketContext(Open(name));
		}
	}
}