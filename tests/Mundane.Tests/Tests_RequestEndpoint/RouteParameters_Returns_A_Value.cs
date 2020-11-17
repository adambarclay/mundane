using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_RequestEndpoint
{
	[ExcludeFromCodeCoverage]
	public static class RouteParameters_Returns_A_Value
	{
		[Fact]
		public static void Which_Was_Passed_To_The_Constructor()
		{
			var p = new[]
			{
				new KeyValuePair<string, string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
				new KeyValuePair<string, string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
			};

			var routing = new Routing(o => o.Get("/{" + p[0].Key + "}/{" + p[1].Key + "}", Response.Ok));

			var requestEndpoint = routing.FindEndpoint(HttpMethod.Get, "/" + p[0].Value + "/" + p[1].Value);

			Assert.Equal(p, requestEndpoint.RouteParameters);
		}
	}
}
