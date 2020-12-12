using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedAsyncExtensions
{
	[ExcludeFromCodeCoverage]
	public static class Validate_Async_Throws_ValidationReturnedNull
	{
		[Fact]
		public static async Task When_The_Async_Task_Parameter_Is_Null()
		{
			var exception = await Assert.ThrowsAnyAsync<ValidationReturnedNull>(
				async () => await ValueTask.FromResult((Validated<string>)null!)
					.Validate(value => ValueTask.FromResult(true), "Error Message."));

			Assert.Equal("The validation returned null.", exception.Message);
		}
	}
}
