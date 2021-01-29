using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class Validate_Async_Throws_ArgumentNullException
	{
		[Fact]
		public static async Task When_The_ErrorMessage_Parameter_Is_Null()
		{
			var value = Validator.Validate(validator => validator.Value(Guid.NewGuid().ToString())).Model;

			var exception = await Assert.ThrowsAnyAsync<ArgumentNullException>(
				async () => await value.Validate(_ => ValueTask.FromResult(true), null!));

			Assert.Equal("errorMessage", exception.ParamName!);
		}

		[Fact]
		public static async Task When_The_Predicate_Parameter_Is_Null()
		{
			var value = Validator.Validate(validator => validator.Value(Guid.NewGuid().ToString())).Model;

			var exception = await Assert.ThrowsAnyAsync<ArgumentNullException>(
				async () => await value.Validate((null as ValidationPredicateAsync<string>)!, "Error Message"));

			Assert.Equal("predicate", exception.ParamName!);
		}
	}
}
