using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedAsyncExtensions
{
	[ExcludeFromCodeCoverage]
	public static class When_Mixing_Async_And_Sync_Validations
	{
		[Fact]
		public static async Task Exceptions_Are_Only_Thrown_On_Await()
		{
			Validated<string> value = string.Empty;

			var erroringValidator = (ValidationPredicateDelegate<string>)(_ => throw new InvalidOperationException());

			var task = value.Validate(_ => ValueTask.FromResult(true), "Error Message")
				.Validate(erroringValidator, "Error Message");

			await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await task);
		}
	}
}
