using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class Operator_NotEquals_Returns_False
	{
		[Fact]
		public static void When_Both_Values_Are_Null()
		{
			Validator.Validate(
				_ =>
				{
					var first = (Validated<string>?)null;
					var second = (Validated<string>?)null;

					Assert.False(first != second);

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

					Assert.False(first != second);

					return string.Empty;
				});
		}
	}
}
