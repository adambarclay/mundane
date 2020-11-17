using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Xunit;

namespace Mundane.Tests.Tests_Request
{
	[ExcludeFromCodeCoverage]
	public static class HeaderExists_Returns_True
	{
		[Fact]
		public static void When_The_Header_Name_Is_In_The_Collection()
		{
			var headerName = Guid.NewGuid().ToString();

			var headers = new Dictionary<string, string> { { headerName, Guid.NewGuid().ToString() } };

			using (var body = new MemoryStream())
			{
				var request = new Request(
					HttpMethod.Get,
					"/",
					new Dictionary<string, string>(0),
					headers,
					body,
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					new Dictionary<string, FileUpload>(0),
					new Dependencies(),
					new RequestHost(string.Empty, string.Empty, string.Empty),
					CancellationToken.None);

				Assert.True(request.HeaderExists(headerName));
			}
		}
	}
}
