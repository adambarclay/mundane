using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Xunit;

namespace Mundane.Tests.Tests_Request
{
	[ExcludeFromCodeCoverage]
	public static class QueryExists_Returns_False
	{
		[Fact]
		public static void When_The_Query_Parameter_Is_Not_In_The_Collection()
		{
			var queryParameter = Guid.NewGuid().ToString();

			var query = new Dictionary<string, string> { { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() } };

			using (var body = new MemoryStream())
			{
				var request = new Request(
					HttpMethod.Get,
					"/",
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					body,
					query,
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					new Dictionary<string, FileUpload>(0),
					new Dependencies(),
					new RequestHost(string.Empty, string.Empty, string.Empty),
					CancellationToken.None);

				Assert.False(request.QueryExists(queryParameter));
			}
		}
	}
}
