using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Moq;
using Xunit;

namespace Mundane.Tests.Tests_Request
{
	[ExcludeFromCodeCoverage]
	public static class File_Returns_A_Value
	{
		[Fact]
		public static void When_The_File_Parameter_Is_In_The_Collection()
		{
			var fileParameter = Guid.NewGuid().ToString();
			var file = new Mock<FileUpload>(MockBehavior.Strict).Object;

			var files = new Dictionary<string, FileUpload> { { fileParameter, file } };

			using (var body = new MemoryStream())
			{
				var request = new Request(
					HttpMethod.Get,
					"/",
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					body,
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					files,
					new Dependencies(),
					new RequestHost(string.Empty, string.Empty, string.Empty),
					CancellationToken.None);

				Assert.Equal(file, request.File(fileParameter));
			}
		}
	}
}