using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class ImplicitOperatorValidatedT_Returns_Correct_Value
	{
		[Fact]
		public static void When_Value_Is_Null()
		{
			var value = ImplicitOperatorValidatedT_Returns_Correct_Value.CreateNull();

			Validated<string> implicitValidatedValue = value;

			Assert.Null(implicitValidatedValue);
		}

		[Fact]
		public static void When_Value_Is_Valid()
		{
			var value = Guid.NewGuid().ToString();

			Validated<string> implicitValidatedValue = value;

			Assert.Equal(Validator.Validate(validator => validator.Value(value)).Model, implicitValidatedValue);
		}

		private static string CreateNull()
		{
			return null!;
		}
	}
}
