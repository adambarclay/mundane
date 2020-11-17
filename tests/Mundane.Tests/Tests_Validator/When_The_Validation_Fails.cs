using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validator
{
	[ExcludeFromCodeCoverage]
	public static class When_The_Validation_Fails
	{
		private const string ErrorMessage = "Value must be greater than zero.";
		private const int Expected = -1;

		[Fact]
		public static void Invalid_Returns_True()
		{
			var result = Validator.Validate(validator => new TestModel(validator, When_The_Validation_Fails.Expected));

			Assert.True(result.Invalid);
		}

		[Fact]
		public static void The_Validated_Value_Has_An_Error_Message()
		{
			var result = Validator.Validate(validator => new TestModel(validator, When_The_Validation_Fails.Expected));

			var model = result.Model;

			Assert.Single(model.Number.ErrorMessages);
			Assert.Equal(When_The_Validation_Fails.ErrorMessage, model.Number.ErrorMessages[0]);
		}

		[Fact]
		public static void The_Validated_Value_Is_Set()
		{
			var result = Validator.Validate(validator => new TestModel(validator, When_The_Validation_Fails.Expected));

			Assert.Equal(When_The_Validation_Fails.Expected, (int)result.Model.Number);
		}

		private sealed class TestModel
		{
			internal TestModel(Validator validator, int number)
			{
				this.Number = validator.Value(number).Validate(o => o > 0, When_The_Validation_Fails.ErrorMessage);
			}

			internal Validated<int> Number { get; }
		}
	}
}
