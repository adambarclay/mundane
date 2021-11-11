using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Routing;

[ExcludeFromCodeCoverage]
public static class AllowedMethodsForPath_Return_Value_Includes_The_Head_Method
{
	[Fact]
	public static void When_A_Get_Method_Route_Matches_The_Path_And_A_Head_Route_Has_Been_Added()
	{
		var path = "/" + Guid.NewGuid();

		var routing = new Routing(
			o =>
			{
				o.Get("/{capture}", Response.Ok);
				o.Post("/{capture}", Response.Ok);
				o.Endpoint(HttpMethod.Head, "/{capture}", Response.Ok);
			});

		var methods = routing.AllowedMethodsForPath(path);

		Assert.Equal(3, methods.Length);
		Assert.Contains(HttpMethod.Get, methods);
		Assert.Contains(HttpMethod.Head, methods);
		Assert.Contains(HttpMethod.Post, methods);
	}

	[Fact]
	public static void When_A_Get_Method_Route_Matches_The_Path_And_No_Head_Routes_Have_Been_Added()
	{
		var path = "/" + Guid.NewGuid();

		var routing = new Routing(
			o =>
			{
				o.Get("/{capture}", Response.Ok);
				o.Post("/{capture}", Response.Ok);
			});

		var methods = routing.AllowedMethodsForPath(path);

		Assert.Equal(3, methods.Length);
		Assert.Contains(HttpMethod.Get, methods);
		Assert.Contains(HttpMethod.Head, methods);
		Assert.Contains(HttpMethod.Post, methods);
	}

	[Fact]
	public static void When_A_Non_Get_Method_Route_Matches_The_Path_And_A_Head_Route_Has_Been_Added()
	{
		var path = "/" + Guid.NewGuid();

		var routing = new Routing(
			o =>
			{
				o.Post("/{capture}", Response.Ok);
				o.Endpoint(HttpMethod.Head, "/{capture}", Response.Ok);
			});

		var methods = routing.AllowedMethodsForPath(path);

		Assert.Equal(2, methods.Length);
		Assert.Contains(HttpMethod.Head, methods);
		Assert.Contains(HttpMethod.Post, methods);
	}
}
