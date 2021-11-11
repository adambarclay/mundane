using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validated;

[ExcludeFromCodeCoverage]
public static class ErrorMessages_Returns_All_Errors
{
	[Fact]
	public static void When_Initialised_With_A_String_And_Errors_Have_Been_Added()
	{
		var errors = new[] { "Error 1", "Error 2" };

		Validated<string> value = Guid.NewGuid().ToString();

		value.AddErrorMessage(errors[0]);
		value.AddErrorMessage(errors[1]);

		Assert.Equal(errors, value.ErrorMessages);
	}

	[Fact]
	public static void When_Initialised_With_A_Value_And_Errors_Have_Been_Added()
	{
		Validator.Validate(
			validator =>
			{
				var errors = new[] { "Error 1", "Error 2" };

				var value = validator.Value(Guid.NewGuid().ToString());

				value.AddErrorMessage(errors[0]);
				value.AddErrorMessage(errors[1]);

				Assert.Equal(errors, value.ErrorMessages);

				return value;
			});
	}
}
