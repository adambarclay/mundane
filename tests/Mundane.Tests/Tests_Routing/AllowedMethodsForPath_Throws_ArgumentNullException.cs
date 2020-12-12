using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Routing
{
	[ExcludeFromCodeCoverage]
	public static class AllowedMethodsForPath_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Path_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => new Routing(o => { }).AllowedMethodsForPath(null!));

			Assert.Equal("path", exception.ParamName!);
		}
	}
}
