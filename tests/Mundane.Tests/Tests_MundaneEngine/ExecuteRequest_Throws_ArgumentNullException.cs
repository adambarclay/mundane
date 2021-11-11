using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngine;

[ExcludeFromCodeCoverage]
public static class ExecuteRequest_Throws_ArgumentNullException
{
	[Fact]
	public static async Task When_The_Endpoint_Parameter_Is_Null()
	{
		var exception = await Assert.ThrowsAnyAsync<ArgumentNullException>(
			async () => await MundaneEngine.ExecuteRequest(null!, RequestHelper.Request()));

		Assert.Equal("endpoint", exception.ParamName!);
	}

	[Fact]
	public static async Task When_The_Request_Parameter_Is_Null()
	{
		var exception = await Assert.ThrowsAnyAsync<ArgumentNullException>(
			async () => await MundaneEngine.ExecuteRequest(MundaneEndpointFactory.Create(Response.Ok), null!));

		Assert.Equal("request", exception.ParamName!);
	}
}
