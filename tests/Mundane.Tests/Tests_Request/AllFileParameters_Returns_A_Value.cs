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
	public static class AllFileParameters_Returns_A_Value
	{
		[Fact]
		public static void Which_Was_Passed_To_The_Constructor()
		{
			var uploadedFiles = new Dictionary<string, FileUpload>
			{
				{ Guid.NewGuid().ToString(), new Mock<FileUpload>(MockBehavior.Strict).Object },
				{ Guid.NewGuid().ToString(), new Mock<FileUpload>(MockBehavior.Strict).Object },
				{ Guid.NewGuid().ToString(), new Mock<FileUpload>(MockBehavior.Strict).Object }
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
					uploadedFiles,
					new Dependencies(),
					new RequestHost(string.Empty, string.Empty, string.Empty),
					CancellationToken.None);

				Assert.Equal(uploadedFiles, request.AllFileParameters);
			}
		}
	}
}
