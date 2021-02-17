using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Validator
{
	[ExcludeFromCodeCoverage]
	public static class When_The_Async_Validation_Succeeds
	{
		[Fact]
		public static async Task Invalid_Returns_False()
		{
			var result = await Validator.Validate(validator => ValueTask.FromResult(new TestModel(validator, 42)));

			Assert.False(result.Invalid);
		}

		[Fact]
		public static async Task The_Validated_Value_Has_No_Error_Messages()
		{
			(var _, var model) = await Validator.Validate(
				validator => ValueTask.FromResult(new TestModel(validator, 42)));

			Assert.Empty(model.Number.ErrorMessages);
		}

		[Fact]
		public static async Task The_Validated_Value_Is_Set()
		{
			var number = RandomNumberGenerator.GetInt32(0, int.MaxValue);

			var result = await Validator.Validate(validator => ValueTask.FromResult(new TestModel(validator, number)));

			Assert.Equal(number, (int)result.Model.Number);
		}

		private sealed class TestModel
		{
			internal TestModel(Validator validator, int number)
			{
				this.Number = validator.Value(number).Validate(o => o > 0, "Value must be greater than zero.");
			}

			internal Validated<int> Number { get; }
		}
	}
}
