using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT;

[ExcludeFromCodeCoverage]
public static class Operator_Equals_Returns_False
{
	[Fact]
	public static void When_The_First_Value_Is_Null()
	{
		Validator.Validate(
			validator =>
			{
				var first = (Validated<string>?)null;
				var second = validator.Value(Guid.NewGuid().ToString());

				Assert.False(first == second);

				return string.Empty;
			});
	}

	[Fact]
	public static void When_The_Second_Value_Is_Null()
	{
		Validator.Validate(
			validator =>
			{
				var first = validator.Value(Guid.NewGuid().ToString());
				var second = (Validated<string>?)null;

				Assert.False(first == second);

				return string.Empty;
			});
	}

	[Fact]
	public static void When_The_Values_Are_Different()
	{
		Validator.Validate(
			validator =>
			{
				var first = validator.Value(Guid.NewGuid().ToString());
				var second = validator.Value(Guid.NewGuid().ToString());

				Assert.False(first == second);

				return string.Empty;
			});
	}
}
