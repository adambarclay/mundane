using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class Validate_Adds_All_Error_Messages
	{
		[Fact]
		public static void When_More_Than_One_Validation_Fails()
		{
			var errorMessages = new[] { "Error Message 1", "Error Message 2" };

			var validatedValue = Validator.Validate(
				validator => validator.Value(Guid.NewGuid().ToString())
					.Validate(_ => false, errorMessages[0])
					.Validate(_ => true, "Dummy Error Message")
					.Validate(_ => false, errorMessages[1]));

			Assert.Equal(errorMessages, validatedValue.Model.ErrorMessages);
		}

		[Fact]
		public static async Task When_More_Than_One_Validation_Including_Async_Fails()
		{
			var errorMessages = new[] { "Error Message 1", "Error Message 2" };

			var validatedValueAsync = await Validator.Validate(
				validator => validator.Value(Guid.NewGuid().ToString())
					.Validate(_ => ValueTask.FromResult(false), errorMessages[0])
					.Validate(_ => ValueTask.FromResult(true), "Dummy Error Message")
					.Validate(_ => false, errorMessages[1]));

			Assert.Equal(errorMessages, validatedValueAsync.Model.ErrorMessages);
		}
	}
}
