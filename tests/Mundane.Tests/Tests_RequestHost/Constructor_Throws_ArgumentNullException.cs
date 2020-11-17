using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_RequestHost
{
	[ExcludeFromCodeCoverage]
	public static class Constructor_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_HostName_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => new RequestHost("scheme", null!, "pathBase"));

			Assert.Equal("hostName", exception.ParamName);
		}

		[Fact]
		public static void When_The_PathBase_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => new RequestHost("scheme", "hostName", null!));

			Assert.Equal("pathBase", exception.ParamName);
		}

		[Fact]
		public static void When_The_Scheme_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => new RequestHost(null!, "hostName", "pathBase"));

			Assert.Equal("scheme", exception.ParamName);
		}
	}
}
