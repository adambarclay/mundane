using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Validator;

[ExcludeFromCodeCoverage]
public static class Validate_Async_Throws_ValidationReturnedNull
{
	[Fact]
	public static async Task When_The_Validation_Returns_Null()
	{
		var exception = await Assert.ThrowsAnyAsync<ValidationReturnedNull>(
			async () => await Validator.Validate(_ => ValueTask.FromResult((object)null!)));

		Assert.Equal("The validation returned null.", exception.Message);
	}
}
