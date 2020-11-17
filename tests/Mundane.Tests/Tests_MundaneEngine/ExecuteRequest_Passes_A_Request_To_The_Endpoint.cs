using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_MundaneEngine
{
	[ExcludeFromCodeCoverage]
	public static class ExecuteRequest_Passes_A_Request_To_The_Endpoint
	{
		[Fact]
		public static async Task Identical_To_The_One_Passed_To_ExecuteRequest()
		{
			var value = Guid.NewGuid().ToString();

			await using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(value)))
			{
				var request = RequestHelper.Request(
					HttpMethod.Get,
					"/",
					new Dictionary<string, string>(),
					new Dictionary<string, string>(),
					memoryStream);

				var response = await MundaneEngine.ExecuteRequest(
					MundaneEndpoint.Create(r => Response.Ok(o => o.Write(r.Body))),
					request);

				Assert.Equal(value, await ResponseHelper.Body(response));
			}
		}
	}
}
