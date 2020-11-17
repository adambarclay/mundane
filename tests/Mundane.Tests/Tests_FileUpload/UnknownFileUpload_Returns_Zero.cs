using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_FileUpload
{
	[ExcludeFromCodeCoverage]
	public static class UnknownFileUpload_Returns_Zero
	{
		[Fact]
		public static void From_Length()
		{
			Assert.Equal(0, FileUpload.Unknown.Length);
		}
	}
}
