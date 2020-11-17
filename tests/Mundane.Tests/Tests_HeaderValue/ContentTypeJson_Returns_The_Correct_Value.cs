using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class ContentTypeJson_Returns_The_Correct_Value
	{
		[Fact]
		public static void Always()
		{
			var header = HeaderValue.ContentTypeJson();

			Assert.Equal("content-type", header.Name);
			Assert.Equal("application/json", header.Value);
		}
	}
}
