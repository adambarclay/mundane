using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Xunit;

namespace Mundane.Tests.Tests_Request
{
	[ExcludeFromCodeCoverage]
	public static class FormExists_Returns_True
	{
		[Fact]
		public static void When_The_Form_Parameter_Is_In_The_Collection()
		{
			var formParameter = Guid.NewGuid().ToString();

			var form = new Dictionary<string, string> { { formParameter, Guid.NewGuid().ToString() } };

			using (var body = new MemoryStream())
			{
				var request = new Request(
					HttpMethod.Get,
					"/",
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					body,
					new Dictionary<string, string>(0),
					form,
					new Dictionary<string, string>(0),
					new Dictionary<string, FileUpload>(0),
					new Dependencies(),
					new RequestHost(string.Empty, string.Empty, string.Empty),
					CancellationToken.None);

				Assert.True(request.FormExists(formParameter));
			}
		}
	}
}