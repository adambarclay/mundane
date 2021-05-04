using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_RouteParameters
{
	[ExcludeFromCodeCoverage]
	public static class Constructor_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_RouteParameters_Parameter_Is_Null()
		{
			Assert.ThrowsAny<ArgumentNullException>(() => new RouteParameters(null!));
		}
	}
}
