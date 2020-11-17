using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Routing
{
	[ExcludeFromCodeCoverage]
	public static class AllowedMethodsForPath_Return_Value_Is_Sorted
	{
		[Fact]
		public static void In_Alphabetical_Order()
		{
			var path = "/" + Guid.NewGuid();

			var routing = new Routing(
				o =>
				{
					o.Post("/{capture}", Response.Ok);
					o.Get("/{capture}", Response.Ok);
					o.Put("/{capture}", Response.Ok);
					o.Delete("/{capture}", Response.Ok);
				});

			var methods = routing.AllowedMethodsForPath(path);

			Assert.Equal(5, methods.Length);
			Assert.Equal(HttpMethod.Delete, methods[0]);
			Assert.Equal(HttpMethod.Get, methods[1]);
			Assert.Equal(HttpMethod.Head, methods[2]);
			Assert.Equal(HttpMethod.Post, methods[3]);
			Assert.Equal(HttpMethod.Put, methods[4]);
		}
	}
}
