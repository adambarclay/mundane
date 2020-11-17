using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class Validate_Async_Throws_ValidationReturnedNull
	{
		[Fact]
		public static async Task When_The_Delegate_Returns_A_Null_Task()
		{
			var value = Validator.Validate(validator => validator.Value(Guid.NewGuid().ToString())).Model;

			var exception = await Assert.ThrowsAnyAsync<ValidationReturnedNull>(
				async () => await value.Validate(x => null!, "Error Message"));

			Assert.Equal("The validation returned a null Task<bool>.", exception.Message);
		}
	}
}
