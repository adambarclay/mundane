using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Response
{
	[ExcludeFromCodeCoverage]
	public static class File_When_Called_With_A_File_Name
	{
		[Fact]
		public static async Task Adds_The_Content_Disposition_Header_For_The_File_Name()
		{
			var fileName = Guid.NewGuid().ToString();

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(
					() => Response.File(o => Task.CompletedTask, Guid.NewGuid().ToString(), fileName)),
				RequestHelper.Request());

			Assert.Equal(2, response.Headers.Count());

			Assert.Equal(
				"attachment;filename=\"" + fileName + "\"",
				response.Headers.First(o => o.Name == "content-disposition").Value);
		}

		[Fact]
		public static async Task Adds_The_Content_Type_Header_For_The_Value_Passed_To_It()
		{
			var contentType = Guid.NewGuid().ToString();

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(
					() => Response.File(o => Task.CompletedTask, contentType, Guid.NewGuid().ToString())),
				RequestHelper.Request());

			Assert.Equal(2, response.Headers.Count());
			Assert.Equal(contentType, response.Headers.First(o => o.Name == "content-type").Value);
		}

		[Fact]
		public static async Task Sets_The_BodyWriter_To_The_Value_Passed_To_It()
		{
			var output = Guid.NewGuid().ToString();

			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(
					() => Response.File(o => o.Write(output), Guid.NewGuid().ToString(), Guid.NewGuid().ToString())),
				RequestHelper.Request());

			Assert.Equal(output, await ResponseHelper.Body(response));
		}

		[Fact]
		public static async Task Sets_The_Status_Code_To_200()
		{
			var response = await MundaneEngine.ExecuteRequest(
				MundaneEndpoint.Create(
					() => Response.File(o => Task.CompletedTask, Guid.NewGuid().ToString(), Guid.NewGuid().ToString())),
				RequestHelper.Request());

			Assert.Equal(200, response.StatusCode);
		}
	}
}
