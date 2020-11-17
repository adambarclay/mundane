using System;
using System.Diagnostics;

namespace Mundane
{
	/// <summary>The exception thrown when a null response is returned from a validation.</summary>
	public sealed class ValidationReturnedNull : Exception
	{
		internal ValidationReturnedNull(string message)
			: base(ValidationReturnedNull.CreateMessage(message))
		{
		}

		private static string CreateMessage(string message)
		{
			Debug.Assert(!message.AsSpan().Trim().IsEmpty);

			return message;
		}
	}
}
