using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Response
{
	[ExcludeFromCodeCoverage]
	public static class AddHeader_Adds_The_Header
	{
		[Fact]
		public static async Task When_A_HeaderValue_With_The_Same_Name_Has_Already_Been_Added()
		{
			var name = Guid.NewGuid().ToString();
			var value1 = Guid.NewGuid().ToString();
			var value2 = Guid.NewGuid().ToString();

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpointFactory.Create(
					() => Response.Ok()
						.AddHeader(new HeaderValue(name, value1))
						.AddHeader(new HeaderValue(name, value2))),
				RequestHelper.Request());

			var headers = response.Headers.ToList();

			Assert.Equal(2, headers.Count);
			Assert.Equal(value1, headers[0].Value);
			Assert.Equal(value2, headers[1].Value);
		}

		[Fact]
		public static async Task When_The_HeaderValue_Has_Been_Initialised()
		{
			var name = Guid.NewGuid().ToString();
			var value = Guid.NewGuid().ToString();

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpointFactory.Create(() => Response.Ok().AddHeader(new HeaderValue(name, value))),
				RequestHelper.Request());

			Assert.Single(response.Headers);
			Assert.Equal(value, response.Headers.First(o => o.Name == name).Value);
		}
	}
}
