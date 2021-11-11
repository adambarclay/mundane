using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT;

[ExcludeFromCodeCoverage]
public static class Validate_Adds_ErrorMessages
{
	[Fact]
	public static void When_Validation_Fails()
	{
		var message = Guid.NewGuid().ToString();

		(var _, var model) = Validator.Validate(
			validator => validator.Value(Guid.NewGuid().ToString()).Validate(_ => false, message));

		Assert.Single(model.ErrorMessages);
		Assert.Equal(message, model.ErrorMessages[0]);
	}
}
