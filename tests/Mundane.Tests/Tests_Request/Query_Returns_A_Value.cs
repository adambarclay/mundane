using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Xunit;

namespace Mundane.Tests.Tests_Request
{
	[ExcludeFromCodeCoverage]
	public static class Query_Returns_A_Value
	{
		[Fact]
		public static void When_The_Query_Parameter_Is_In_The_Collection()
		{
			var queryParameter = Guid.NewGuid().ToString();
			var queryValue = Guid.NewGuid().ToString();

			var query = new Dictionary<string, string> { { queryParameter, queryValue } };

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

				Assert.Equal(queryValue, request.Query(queryParameter));
			}
		}
	}
}
