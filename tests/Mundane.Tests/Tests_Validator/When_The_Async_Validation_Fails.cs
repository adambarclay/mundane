using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Validator;

[ExcludeFromCodeCoverage]
public static class When_The_Async_Validation_Fails
{
	[Fact]
	public static async Task Invalid_Returns_True()
	{
		var result = await Validator.Validate(validator => ValueTask.FromResult(new TestModel(validator, -1)));

		Assert.True(result.Invalid);
	}

	[Fact]
	public static async Task The_Validated_Value_Has_An_Error_Message()
	{
		var result = await Validator.Validate(validator => ValueTask.FromResult(new TestModel(validator, -1)));

		var model = result.Model;

		Assert.Single(model.Number.ErrorMessages);

		Assert.Equal("Value must be greater than zero.", model.Number.ErrorMessages[0]);
	}

	[Fact]
	public static async Task The_Validated_Value_Is_Set()
	{
		var result = await Validator.Validate(validator => ValueTask.FromResult(new TestModel(validator, -1)));

		Assert.Equal(-1, (int)result.Model.Number);
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
