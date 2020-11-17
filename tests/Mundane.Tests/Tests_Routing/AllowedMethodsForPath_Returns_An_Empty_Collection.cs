using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Routing
{
	[ExcludeFromCodeCoverage]
	public static class AllowedMethodsForPath_Returns_An_Empty_Collection
	{
		[Fact]
		public static void When_No_Method_Matches_The_Path()
		{
			var path = "/" + Guid.NewGuid();

			var routing = new Routing(o => o.Post("/" + Guid.NewGuid(), Response.Ok));

			var methods = routing.AllowedMethodsForPath(path);

			Assert.Empty(methods);
		}
	}
}
