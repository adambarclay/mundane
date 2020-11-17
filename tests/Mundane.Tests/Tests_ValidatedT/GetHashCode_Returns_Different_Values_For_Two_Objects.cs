using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class GetHashCode_Returns_Different_Values_For_Two_Objects
	{
		[Fact]
		public static void When_The_Values_Are_Different()
		{
			Validated<string> first = Guid.NewGuid().ToString();
			Validated<string> second = Guid.NewGuid().ToString();

			Assert.NotEqual(first.GetHashCode(), second.GetHashCode());
		}
	}
}
