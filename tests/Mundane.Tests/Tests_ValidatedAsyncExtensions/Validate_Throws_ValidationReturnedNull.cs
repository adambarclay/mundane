using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedAsyncExtensions
{
	[ExcludeFromCodeCoverage]
	public static class Validate_Throws_ValidationReturnedNull
	{
		[Fact]
		public static async Task When_The_Task_Parameter_Is_Null()
		{
			var exception = await Assert.ThrowsAnyAsync<ValidationReturnedNull>(
				async () => await Task.FromResult((Validated<string>)null!).Validate(value => true, "Error Message."));

			Assert.Equal("The validation returned null.", exception.Message);
		}
	}
}
