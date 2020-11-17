using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Xunit;

namespace Mundane.Tests.Tests_Request
{
	[ExcludeFromCodeCoverage]
	public static class RequestAborted_Returns_A_Value
	{
		[Fact]
		public static void Which_Was_Passed_To_The_Constructor()
		{
			var requestAborted = new CancellationToken(true);

			using (var body = new MemoryStream())
			{
				var request = new Request(
					HttpMethod.Get,
					"/",
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					body,
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					new Dictionary<string, FileUpload>(0),
					new Dependencies(),
					new RequestHost(string.Empty, string.Empty, string.Empty),
					requestAborted);

				Assert.Equal(requestAborted, request.RequestAborted);
			}
		}
	}
}
