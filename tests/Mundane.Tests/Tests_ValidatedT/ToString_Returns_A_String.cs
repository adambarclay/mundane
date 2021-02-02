using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class ToString_Returns_A_String
	{
		[Fact]
		public static void Equal_To_Empty_String_When_Value_Is_Null()
		{
			string value = null!;

			Validated<string> validatedValue = value;

			Assert.Equal(string.Empty, validatedValue.ToString());
		}

		[Fact]
		public static void Identical_To_The_String_Used_To_Initialise_The_Value()
		{
			var value = Guid.NewGuid().ToString();

			Validated<int> validatedValue = value;

			Assert.Equal(value, validatedValue.ToString());
		}
	}
}
