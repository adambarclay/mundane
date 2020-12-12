using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngineResponse
{
	[ExcludeFromCodeCoverage]
	public static class Operator_Equals_Returns_True
	{
		[Fact]
		public static async Task When_The_Objects_Have_The_Same_Values()
		{
			var request = RequestHelper.Request();
			const int statusCode = 200;
			BodyWriter bodyWriter = o => o.Write("Hello World!");

			var headers = new[]
			{
				new KeyValuePair<string, string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())
			};

			var first = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(
					() => new Response(statusCode, bodyWriter).AddHeader(
						new HeaderValue(headers[0].Key, headers[0].Value))),
				request);

			var second = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(
					() => new Response(statusCode, bodyWriter).AddHeader(
						new HeaderValue(headers[0].Key, headers[0].Value))),
				request);

			Assert.True(first == second);
		}
	}
}
