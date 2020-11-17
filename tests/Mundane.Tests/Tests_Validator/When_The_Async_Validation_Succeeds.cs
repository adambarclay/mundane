using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Validator
{
	[ExcludeFromCodeCoverage]
	public static class When_The_Async_Validation_Succeeds
	{
		private const string ErrorMessage = "Value must be greater than zero.";
		private const int Expected = 42;

		[Fact]
		public static async Task Invalid_Returns_False()
		{
			var result = await Validator.Validate(
				validator => Task.FromResult(new TestModel(validator, When_The_Async_Validation_Succeeds.Expected)));

			Assert.False(result.Invalid);
		}

		[Fact]
		public static async Task The_Validated_Value_Has_No_Error_Messages()
		{
			(var _, var model) = await Validator.Validate(
				validator => Task.FromResult(new TestModel(validator, When_The_Async_Validation_Succeeds.Expected)));

			Assert.Empty(model.Number.ErrorMessages);
		}

		[Fact]
		public static async Task The_Validated_Value_Is_Set()
		{
			var result = await Validator.Validate(
				validator => Task.FromResult(new TestModel(validator, When_The_Async_Validation_Succeeds.Expected)));

			Assert.Equal(When_The_Async_Validation_Succeeds.Expected, (int)result.Model.Number);
		}

		private sealed class TestModel
		{
			internal TestModel(Validator validator, int number)
			{
				this.Number = validator.Value(number)
					.Validate(o => o > 0, When_The_Async_Validation_Succeeds.ErrorMessage);
			}

			internal Validated<int> Number { get; }
		}
	}
}
