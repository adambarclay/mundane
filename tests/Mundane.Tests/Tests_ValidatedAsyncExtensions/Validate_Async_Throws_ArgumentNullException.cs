using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedAsyncExtensions
{
	[ExcludeFromCodeCoverage]
	public static class Validate_Async_Throws_ArgumentNullException
	{
		[Fact]
		public static async Task When_The_ErrorMessage_Parameter_Is_Null()
		{
			var exception = await Assert.ThrowsAnyAsync<ArgumentNullException>(
				async () => await ValueTask.FromResult((Validated<string>)string.Empty)
					.Validate(value => ValueTask.FromResult(true), null!));

			Assert.Equal("errorMessage", exception.ParamName!);
		}

		[Fact]
		public static async Task When_The_Predicate_Parameter_Is_Null()
		{
			var exception = await Assert.ThrowsAnyAsync<ArgumentNullException>(
				async () => await ValueTask.FromResult((Validated<string>)string.Empty)
					.Validate((ValidationPredicateDelegateAsync<string>)null!, "Error Message."));

			Assert.Equal("predicate", exception.ParamName!);
		}
	}
}
