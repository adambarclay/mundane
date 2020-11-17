using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class ImplicitOperatorT_Returns_Correct_Value
	{
		[Fact]
		public static void When_Value_Is_Null()
		{
			var value = ImplicitOperatorT_Returns_Correct_Value.CreateNull();

			string implicitString = value;

			Assert.Null(implicitString);
		}

		[Fact]
		public static void When_Value_Is_Valid()
		{
			var value = Guid.NewGuid().ToString();

			string implicitString = Validator.Validate(validator => validator.Value(value)).Model;

			Assert.Equal(value, implicitString);
		}

		private static Validated<string> CreateNull()
		{
			return null!;
		}
	}
}
