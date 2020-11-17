using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class ContentDisposition_Returns_The_Correct_Value
	{
		[Fact]
		public static void When_The_File_Name_Has_Been_Supplied()
		{
			var fileName = Guid.NewGuid().ToString();

			var header = HeaderValue.ContentDisposition(fileName);

			Assert.Equal("content-disposition", header.Name);
			Assert.Equal("attachment;filename=\"" + fileName + "\"", header.Value);
		}
	}
}
