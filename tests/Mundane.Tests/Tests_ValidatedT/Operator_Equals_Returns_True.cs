using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class Operator_Equals_Returns_True
	{
		[Fact]
		public static void When_Both_Values_Are_Null()
		{
			Validator.Validate(
				_ =>
				{
					var first = (Validated<string>?)null;
					var second = (Validated<string>?)null;

					Assert.True(first == second);

					return string.Empty;
				});
		}

		[Fact]
		public static void When_The_Values_Are_The_Same()
		{
			Validator.Validate(
				validator =>
				{
					var value = Guid.NewGuid().ToString();

					var first = validator.Value(value);
					var second = validator.Value(value);

					Assert.True(first == second);

					return string.Empty;
				});
		}
	}
}
