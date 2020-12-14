using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class Validate_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_ErrorMessage_Parameter_Is_Null()
		{
			var value = Validator.Validate(validator => validator.Value(Guid.NewGuid().ToString())).Model;

			var exception = Assert.ThrowsAny<ArgumentNullException>(() => value.Validate(_ => true, null!));

			Assert.Equal("errorMessage", exception.ParamName!);
		}

		[Fact]
		public static void When_The_Predicate_Parameter_Is_Null()
		{
			var value = Validator.Validate(validator => validator.Value(Guid.NewGuid().ToString())).Model;

			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => value.Validate((null as ValidationPredicateDelegate<string>)!, "Error Message"));

			Assert.Equal("predicate", exception.ParamName!);
		}
	}
}
