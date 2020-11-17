using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Xunit;

namespace Mundane.Tests.Tests_Request
{
	[ExcludeFromCodeCoverage]
	public static class Route_Returns_Empty_String
	{
		[Fact]
		public static void When_The_Route_Parameter_Is_Not_In_The_Collection()
		{
			var routeParameter = Guid.NewGuid().ToString();

			var route = new Dictionary<string, string> { { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() } };

			using (var body = new MemoryStream())
			{
				var request = new Request(
					HttpMethod.Get,
					"/",
					route,
					new Dictionary<string, string>(0),
					body,
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					new Dictionary<string, FileUpload>(0),
					new Dependencies(),
					new RequestHost(string.Empty, string.Empty, string.Empty),
					CancellationToken.None);

				Assert.Equal(string.Empty, request.Route(routeParameter));
			}
		}
	}
}