using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class Validate_Adds_ErrorMessages
	{
		[Fact]
		public static void When_Validation_Fails()
		{
			var validatedValue = Validator.Validate(
				validator => validator.Value(Guid.NewGuid().ToString()).Validate(_ => false, "Error Message"));

			Assert.Single(validatedValue.Model.ErrorMessages);
		}
	}
}
