using System;

namespace BTI7252.DataAccess
{
	public interface ICouchConnectionManager
	{
		Uri ApiUrl { get; }
		string Username { get; }
		string Password { get; }
	}
}