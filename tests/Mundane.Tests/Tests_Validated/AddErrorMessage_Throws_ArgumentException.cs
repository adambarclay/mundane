using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validated;

[ExcludeFromCodeCoverage]
public static class AddErrorMessage_Throws_ArgumentException
{
	[Fact]
	public static void When_The_ErrorMessage_Parameter_Is_Empty()
	{
		var exception = Assert.ThrowsAny<ArgumentException>(
			() => Validator.Validate(
				validator =>
				{
					var value = validator.Value(Guid.NewGuid().ToString());

					value.AddErrorMessage(string.Empty);

					return value;
				}));

		Assert.Equal("errorMessage", exception.ParamName!);
	}

	[Fact]
	public static void When_The_ErrorMessage_Parameter_Is_Whitespace()
	{
		var exception = Assert.ThrowsAny<ArgumentException>(
			() => Validator.Validate(
				validator =>
				{
					var value = validator.Value(Guid.NewGuid().ToString());

					value.AddErrorMessage("   ");

					return value;
				}));

		Assert.Equal("errorMessage", exception.ParamName!);
	}
}
