using System;
using System.Diagnostics;

namespace Mundane
{
	/// <summary>The exception thrown when a null response is returned from an endpoint.</summary>
	public sealed class EndpointReturnedNull : Exception
	{
		internal EndpointReturnedNull(string message)
			: base(EndpointReturnedNull.CreateMessage(message))
		{
		}

		private static string CreateMessage(string message)
		{
			Debug.Assert(!message.AsSpan().Trim().IsEmpty);

			return message;
		}
	}
}
