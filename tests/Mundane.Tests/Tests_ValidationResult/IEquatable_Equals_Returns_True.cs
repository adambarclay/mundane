using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidationResult
{
	[ExcludeFromCodeCoverage]
	public static class IEquatable_Equals_Returns_True
	{
		[Fact]
		public static void When_The_Values_Are_The_Same()
		{
			var value = Guid.NewGuid().ToString();

			var first = Validator.Validate(validator => value);
			var second = Validator.Validate(validator => value);

			Assert.True(first.Equals(second));
		}
	}
}
