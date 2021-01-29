using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class Validate_Adds_ErrorMessages
	{
		[Fact]
		public static void When_Validation_Fails()
		{
			var message = Guid.NewGuid().ToString();

			var validatedValue = Validator.Validate(
				validator => validator.Value(Guid.NewGuid().ToString()).Validate(_ => false, message));

			Assert.Single(validatedValue.Model.ErrorMessages);
			Assert.Equal(message, validatedValue.Model.ErrorMessages[0]);
		}
	}
}
