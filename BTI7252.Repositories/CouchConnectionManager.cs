using System;

namespace BTI7252.DataAccess
{
	public class CouchConnectionManager : ICouchConnectionManager
	{
		public CouchConnectionManager(Uri apiUrl, string username, string password)
		{
			ApiUrl = apiUrl;
			Username = username;
			Password = password;
		}

		public Uri ApiUrl { get; }
		public string Username { get; }
		public string Password { get; }
	}
}