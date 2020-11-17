using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validator
{
	[ExcludeFromCodeCoverage]
	public static class Value_Returns_Validated_Object_Wrapper
	{
		[Fact]
		public static void Test()
		{
			var value = Guid.NewGuid().ToString();

			var validatedValue = Validator.Validate(validator => validator.Value(value)).Model;

			Assert.NotNull(validatedValue);
			Assert.IsType<Validated<string>>(validatedValue);
			Assert.Equal(value, validatedValue);
			Assert.Empty(validatedValue.ErrorMessages);
		}
	}
}
