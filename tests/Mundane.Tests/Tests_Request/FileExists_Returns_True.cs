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
	public static class FileExists_Returns_True
	{
		[Fact]
		public static void When_The_File_Parameter_Is_In_The_Collection()
		{
			var fileParameter = Guid.NewGuid().ToString();

			var files = new Dictionary<string, FileUpload>
			{
				{ fileParameter, new Mock<FileUpload>(MockBehavior.Strict).Object }
			};

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

				Assert.True(request.FileExists(fileParameter));
			}
		}
	}
}
