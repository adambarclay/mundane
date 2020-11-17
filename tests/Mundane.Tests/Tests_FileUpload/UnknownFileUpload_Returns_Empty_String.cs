using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_FileUpload
{
	[ExcludeFromCodeCoverage]
	public static class UnknownFileUpload_Returns_Empty_String
	{
		[Fact]
		public static void From_FileName()
		{
			Assert.Equal(string.Empty, FileUpload.Unknown.FileName);
		}

		[Fact]
		public static void From_MediaType()
		{
			Assert.Equal(string.Empty, FileUpload.Unknown.MediaType);
		}
	}
}
