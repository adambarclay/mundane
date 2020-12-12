using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class ToString_Returns_A_String
	{
		[Theory]
		[InlineData(123.456)]
		[InlineData(42)]
		[InlineData("hello")]
		public static void Identical_To_Calling_ToString_On_The_Underlying_Object<T>(T value)
			where T : notnull
		{
			Validated<T> validatedValue = value;

			Assert.Equal(value.ToString()!, validatedValue.ToString()!);
		}
	}
}
