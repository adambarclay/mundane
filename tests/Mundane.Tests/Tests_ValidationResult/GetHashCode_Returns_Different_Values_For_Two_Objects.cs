using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidationResult
{
	[ExcludeFromCodeCoverage]
	public static class GetHashCode_Returns_Different_Values_For_Two_Objects
	{
		[Fact]
		public static void When_The_Values_Are_Different()
		{
			var first = Validator.Validate(_ => Guid.NewGuid().ToString());
			var second = Validator.Validate(_ => Guid.NewGuid().ToString());

			Assert.NotEqual(first.GetHashCode(), second.GetHashCode());
		}
	}
}
