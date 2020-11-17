using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEndpoint
{
	[ExcludeFromCodeCoverage]
	public static class The_Returned_Endpoint_Outcome_Is_Identical_To_The_Original_Endpoint_Outcome
	{
		[Fact]
		public static async Task When_Creating_From_A_MundaneEndpointDelegate()
		{
			var statusCode = new Random().Next();
			var input = Guid.NewGuid();
			var output = Guid.Empty;

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(
					r =>
					{
						output = input;

						return Task.FromResult(new Response(statusCode));
					}),
				RequestHelper.Request());

			Assert.Equal(statusCode, response.StatusCode);
			Assert.Equal(input, output);
		}

		[Fact]
		public static async Task When_Creating_From_A_MundaneEndpointDelegateNoParameters()
		{
			var statusCode = new Random().Next();
			var input = Guid.NewGuid();
			var output = Guid.Empty;

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(
					() =>
					{
						output = input;

						return Task.FromResult(new Response(statusCode));
					}),
				RequestHelper.Request());

			Assert.Equal(statusCode, response.StatusCode);
			Assert.Equal(input, output);
		}

		[Fact]
		public static async Task When_Creating_From_A_MundaneEndpointDelegateNoParametersSync()
		{
			var statusCode = new Random().Next();
			var input = Guid.NewGuid();
			var output = Guid.Empty;

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(
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
		public static async Task When_Creating_From_A_MundaneEndpointDelegateSync()
		{
			var statusCode = new Random().Next();
			var input = Guid.NewGuid();
			var output = Guid.Empty;

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(
					r =>
					{
						output = input;

						return new Response(statusCode);
					}),
				RequestHelper.Request());

			Assert.Equal(statusCode, response.StatusCode);
			Assert.Equal(input, output);
		}
	}
}
