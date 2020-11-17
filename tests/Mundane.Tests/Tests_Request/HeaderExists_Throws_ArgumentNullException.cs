using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Xunit;

namespace Mundane.Tests.Tests_Request
{
	[ExcludeFromCodeCoverage]
	public static class HeaderExists_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_HeaderName_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
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
							CancellationToken.None);

						request.HeaderExists(null!);
					}
				});

			Assert.Equal("headerName", exception.ParamName);
		}
	}
}
