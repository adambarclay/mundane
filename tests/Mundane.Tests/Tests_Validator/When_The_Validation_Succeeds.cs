using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validator
{
	[ExcludeFromCodeCoverage]
	public static class When_The_Validation_Succeeds
	{
		private const string ErrorMessage = "Value must be greater than zero.";
		private const int Expected = 42;

		[Fact]
		public static void Invalid_Returns_False()
		{
			var result = Validator.Validate(
				validator => new TestModel(validator, When_The_Validation_Succeeds.Expected));

			Assert.False(result.Invalid);
		}

		[Fact]
		public static void The_Validated_Value_Has_No_Error_Messages()
		{
			var result = Validator.Validate(
				validator => new TestModel(validator, When_The_Validation_Succeeds.Expected));

			Assert.Empty(result.Model.Number.ErrorMessages);
		}

		[Fact]
		public static void The_Validated_Value_Is_Set()
		{
			var result = Validator.Validate(
				validator => new TestModel(validator, When_The_Validation_Succeeds.Expected));

			Assert.Equal(When_The_Validation_Succeeds.Expected, (int)result.Model.Number);
		}

		private sealed class TestModel
		{
			internal TestModel(Validator validator, int number)
			{
				this.Number = validator.Value(number).Validate(o => o > 0, When_The_Validation_Succeeds.ErrorMessage);
			}

			internal Validated<int> Number { get; }
		}
	}
}
