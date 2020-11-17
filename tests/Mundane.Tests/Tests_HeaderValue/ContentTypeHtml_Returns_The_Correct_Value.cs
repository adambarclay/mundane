using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class ContentTypeHtml_Returns_The_Correct_Value
	{
		[Fact]
		public static void Always()
		{
			var header = HeaderValue.ContentTypeHtml();

			Assert.Equal("content-type", header.Name);
			Assert.Equal("text/html;charset=utf-8", header.Value);
		}
	}
}
