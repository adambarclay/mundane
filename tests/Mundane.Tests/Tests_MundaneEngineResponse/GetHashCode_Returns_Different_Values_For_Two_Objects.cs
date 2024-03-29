using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngineResponse;

[ExcludeFromCodeCoverage]
public static class GetHashCode_Returns_Different_Values_For_Two_Objects
{
	[Fact]
	public static async Task When_The_Objects_Have_Different_Body_Writers()
	{
		var request = RequestHelper.Request();
		const int statusCode = 200;

		var headers = new[] { new KeyValuePair<string, string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()) };

		var first = (await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => new Response(statusCode, o => o.Write("Hello!")).AddHeader(
					new HeaderValue(headers[0].Key, headers[0].Value))),
			request)).GetHashCode();

		var second = (await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => new Response(statusCode, o => o.Flush()).AddHeader(
					new HeaderValue(headers[0].Key, headers[0].Value))),
			request)).GetHashCode();

		Assert.NotEqual(first, second);
	}

	[Fact]
	public static async Task When_The_Objects_Have_Different_Headers()
	{
		var request = RequestHelper.Request();
		const int statusCode = 200;
		BodyWriter bodyWriter = o => o.Write("Hello World!");

		var first = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => new Response(statusCode, bodyWriter).AddHeader(
					new HeaderValue(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()))),
			request);

		var second = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => new Response(statusCode, bodyWriter).AddHeader(
					new HeaderValue(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()))),
			request);

		Assert.NotEqual(first.GetHashCode(), second.GetHashCode());
	}

	[Fact]
	public static async Task When_The_Objects_Have_Different_Requests()
	{
		const int statusCode = 200;
		BodyWriter bodyWriter = o => o.Write("Hello World!");

		var headers = new[] { new KeyValuePair<string, string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()) };

		var first = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => new Response(statusCode, bodyWriter).AddHeader(
					new HeaderValue(headers[0].Key, headers[0].Value))),
			RequestHelper.Request());

		var second = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => new Response(statusCode, bodyWriter).AddHeader(
					new HeaderValue(headers[0].Key, headers[0].Value))),
			RequestHelper.Request());

		Assert.NotEqual(first.GetHashCode(), second.GetHashCode());
	}

	[Fact]
	public static async Task When_The_Objects_Have_Different_Status_Codes()
	{
		var request = RequestHelper.Request();
		BodyWriter bodyWriter = o => o.Write("Hello World!");

		var headers = new[] { new KeyValuePair<string, string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()) };

		var first = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => new Response(200, bodyWriter).AddHeader(new HeaderValue(headers[0].Key, headers[0].Value))),
			request);

		var second = await MundaneEngine.ExecuteRequest(
			MundaneEndpointFactory.Create(
				() => new Response(500, bodyWriter).AddHeader(new HeaderValue(headers[0].Key, headers[0].Value))),
			request);

		Assert.NotEqual(first.GetHashCode(), second.GetHashCode());
	}
}
