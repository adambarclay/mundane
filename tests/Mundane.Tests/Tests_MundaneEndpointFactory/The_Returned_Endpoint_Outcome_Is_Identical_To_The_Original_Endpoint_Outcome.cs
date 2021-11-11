using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEndpointFactory;

[ExcludeFromCodeCoverage]
public static class The_Returned_Endpoint_Outcome_Is_Identical_To_The_Original_Endpoint_Outcome
{
	[Fact]
	public static async Task When_Creating_From_A_MundaneEndpoint()
	{
		var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);
		var input = Guid.NewGuid();
		var output = Guid.Empty;

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				_ =>
				{
					output = input;

					return ValueTask.FromResult(new Response(statusCode));
				}),
			RequestHelper.Request());

		Assert.Equal(statusCode, response.StatusCode);
		Assert.Equal(input, output);
	}

	[Fact]
	public static async Task When_Creating_From_A_MundaneEndpointNoParameters()
	{
		var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);
		var input = Guid.NewGuid();
		var output = Guid.Empty;

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() =>
				{
					output = input;

					return ValueTask.FromResult(new Response(statusCode));
				}),
			RequestHelper.Request());

		Assert.Equal(statusCode, response.StatusCode);
		Assert.Equal(input, output);
	}

	[Fact]
	public static async Task When_Creating_From_A_MundaneEndpointNoParametersSync()
	{
		var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);
		var input = Guid.NewGuid();
		var output = Guid.Empty;

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() =>
				{
					output = input;

					return new Response(statusCode);
				}),
			RequestHelper.Request());

		Assert.Equal(statusCode, response.StatusCode);
		Assert.Equal(input, output);
	}

	[Fact]
	public static async Task When_Creating_From_A_MundaneEndpointSync()
	{
		var statusCode = RandomNumberGenerator.GetInt32(0, int.MaxValue);
		var input = Guid.NewGuid();
		var output = Guid.Empty;

		var response = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				_ =>
				{
					output = input;

					return new Response(statusCode);
				}),
			RequestHelper.Request());

		Assert.Equal(statusCode, response.StatusCode);
		Assert.Equal(input, output);
	}
}
