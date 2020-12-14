using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidationResult
{
	[ExcludeFromCodeCoverage]
	public static class GetHashCode_Returns_The_Same_Value_For_Two_Objects
	{
		[Fact]
		public static void When_The_Values_Are_The_Same()
		{
			var value = Guid.NewGuid().ToString();

			var first = Validator.Validate(_ => value);
			var second = Validator.Validate(_ => value);

			Assert.Equal(first.GetHashCode(), second.GetHashCode());
		}
	}
}
