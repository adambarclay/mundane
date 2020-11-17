using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_FileUpload
{
	[ExcludeFromCodeCoverage]
	public static class UnknownFileUpload_Returns_Empty_Stream
	{
		[Fact]
		public static void From_OpenReadStream()
		{
			using (var stream = FileUpload.Unknown.Open())
			{
				Assert.Equal(0, stream.Length);
			}
		}
	}
}
