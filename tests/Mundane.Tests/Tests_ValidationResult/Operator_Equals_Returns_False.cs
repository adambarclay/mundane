using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidationResult
{
	[ExcludeFromCodeCoverage]
	public static class Operator_Equals_Returns_False
	{
		[Fact]
		public static void When_The_Values_Are_Different()
		{
			var first = Validator.Validate(_ => Guid.NewGuid().ToString());
			var second = Validator.Validate(_ => Guid.NewGuid().ToString());

			Assert.False(first == second);
		}
	}
}
