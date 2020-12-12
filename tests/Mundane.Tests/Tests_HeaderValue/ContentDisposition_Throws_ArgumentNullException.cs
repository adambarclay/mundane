using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class ContentDisposition_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_FileName_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => HeaderValue.ContentDisposition(null!));

			Assert.Equal("fileName", exception.ParamName!);
		}
	}
}
