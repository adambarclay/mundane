using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Xunit;

namespace Mundane.Tests.Tests_Request
{
	[ExcludeFromCodeCoverage]
	public static class Constructor_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Body_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => new Request(
					HttpMethod.Get,
					"/",
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					null!,
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					new Dictionary<string, string>(0),
					new Dictionary<string, FileUpload>(0),
					new Dependencies(),
					new RequestHost(string.Empty, string.Empty, string.Empty),
					CancellationToken.None));

			Assert.Equal("body", exception.ParamName);
		}

		[Fact]
		public static void When_The_Cookies_Parameter_Is_Default()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
					using (var body = new MemoryStream())
					{
						return new Request(
							HttpMethod.Get,
							"/",
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							body,
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							default,
							new Dictionary<string, FileUpload>(0),
							new Dependencies(),
							new RequestHost(string.Empty, string.Empty, string.Empty),
							CancellationToken.None);
					}
				});

			Assert.Equal("cookies", exception.ParamName);
		}

		[Fact]
		public static void When_The_DependencyFinder_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
					using (var body = new MemoryStream())
					{
						return new Request(
							HttpMethod.Get,
							"/",
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							body,
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, FileUpload>(0),
							null!,
							new RequestHost(string.Empty, string.Empty, string.Empty),
							CancellationToken.None);
					}
				});

			Assert.Equal("dependencyFinder", exception.ParamName);
		}

		[Fact]
		public static void When_The_Form_Parameter_Is_Default()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
					using (var body = new MemoryStream())
					{
						return new Request(
							HttpMethod.Get,
							"/",
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							body,
							new Dictionary<string, string>(0),
							default,
							new Dictionary<string, string>(0),
							new Dictionary<string, FileUpload>(0),
							new Dependencies(),
							new RequestHost(string.Empty, string.Empty, string.Empty),
							CancellationToken.None);
					}
				});

			Assert.Equal("form", exception.ParamName);
		}

		[Fact]
		public static void When_The_Headers_Parameter_Is_Default()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
					using (var body = new MemoryStream())
					{
						return new Request(
							HttpMethod.Get,
							"/",
							new Dictionary<string, string>(0),
							default,
							body,
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, FileUpload>(0),
							new Dependencies(),
							new RequestHost(string.Empty, string.Empty, string.Empty),
							CancellationToken.None);
					}
				});

			Assert.Equal("headers", exception.ParamName);
		}

		[Fact]
		public static void When_The_Host_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
					using (var body = new MemoryStream())
					{
						return new Request(
							HttpMethod.Get,
							"/",
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							body,
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, FileUpload>(0),
							new Dependencies(),
							null!,
							CancellationToken.None);
					}
				});

			Assert.Equal("host", exception.ParamName);
		}

		[Fact]
		public static void When_The_Method_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
					using (var body = new MemoryStream())
					{
						return new Request(
							null!,
							"/",
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							body,
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, FileUpload>(0),
							new Dependencies(),
							new RequestHost(string.Empty, string.Empty, string.Empty),
							CancellationToken.None);
					}
				});

			Assert.Equal("method", exception.ParamName);
		}

		[Fact]
		public static void When_The_Path_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
					using (var body = new MemoryStream())
					{
						return new Request(
							HttpMethod.Get,
							null!,
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							body,
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, FileUpload>(0),
							new Dependencies(),
							new RequestHost(string.Empty, string.Empty, string.Empty),
							CancellationToken.None);
					}
				});

			Assert.Equal("path", exception.ParamName);
		}

		[Fact]
		public static void When_The_Query_Parameter_Is_Default()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
					using (var body = new MemoryStream())
					{
						return new Request(
							HttpMethod.Get,
							"/",
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							body,
							default,
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, FileUpload>(0),
							new Dependencies(),
							new RequestHost(string.Empty, string.Empty, string.Empty),
							CancellationToken.None);
					}
				});

			Assert.Equal("query", exception.ParamName);
		}

		[Fact]
		public static void When_The_Route_Parameter_Is_Default()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
					using (var body = new MemoryStream())
					{
						return new Request(
							HttpMethod.Get,
							"/",
							default,
							new Dictionary<string, string>(0),
							body,
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, FileUpload>(0),
							new Dependencies(),
							new RequestHost(string.Empty, string.Empty, string.Empty),
							CancellationToken.None);
					}
				});

			Assert.Equal("route", exception.ParamName);
		}

		[Fact]
		public static void When_The_UploadedFiles_Parameter_Is_Default()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
					using (var body = new MemoryStream())
					{
						return new Request(
							HttpMethod.Get,
							"/",
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							body,
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							new Dictionary<string, string>(0),
							default,
							new Dependencies(),
							new RequestHost(string.Empty, string.Empty, string.Empty),
							CancellationToken.None);
					}
				});

			Assert.Equal("uploadedFiles", exception.ParamName);
		}
	}
}
