using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT;

[ExcludeFromCodeCoverage]
public static class Validate_Does_Not_Add_Error_Message
{
	[Fact]
	public static void When_Validation_Succeeds()
	{
		var validatedValue = Validator.Validate(
			validator => validator.Value(Guid.NewGuid().ToString()).Validate(_ => true, "Error Message"));

		Assert.Empty(validatedValue.Model.ErrorMessages);
	}
}
