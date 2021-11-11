using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Routing;

[ExcludeFromCodeCoverage]
public static class AllowedMethodsForPath_Returns_One_Of_Each_Method_Which_Matches_The_Path
{
	[Fact]
	public static void When_More_Than_One_Route_For_The_Same_Method_Matches_The_Path()
	{
		var path = "/" + Guid.NewGuid();

		var routing = new Routing(
			o =>
			{
				o.Post("/{capture}", Response.Ok);
				o.Post(path, Response.Ok);
			});

		var methods = routing.AllowedMethodsForPath(path);

		Assert.Single(methods);
		Assert.Contains(HttpMethod.Post, methods);
	}

	[Fact]
	public static void When_Multiple_Methods_Match_The_Path()
	{
		var path = "/" + Guid.NewGuid();

		var routing = new Routing(
			o =>
			{
				o.Delete("/{capture}", Response.Ok);
				o.Post("/{capture}", Response.Ok);
			});

		var methods = routing.AllowedMethodsForPath(path);

		Assert.Equal(2, methods.Length);
		Assert.Contains(HttpMethod.Delete, methods);
		Assert.Contains(HttpMethod.Post, methods);
	}

	[Fact]
	public static void When_One_Method_Matches_The_Path()
	{
		var path = "/" + Guid.NewGuid();

		var routing = new Routing(o => o.Post("/{capture}", Response.Ok));

		var methods = routing.AllowedMethodsForPath(path);

		Assert.Single(methods);
		Assert.Contains(HttpMethod.Post, methods);
	}
}
