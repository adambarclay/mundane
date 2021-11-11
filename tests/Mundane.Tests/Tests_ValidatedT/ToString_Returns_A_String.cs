using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT;

[ExcludeFromCodeCoverage]
public static class ToString_Returns_A_String
{
	[Fact]
	public static void Identical_To_The_String_Used_To_Initialise_The_DisplayString()
	{
		var value = Guid.NewGuid().ToString();

		Validated<int> validatedValue = (0, value);

		Assert.Equal(value, validatedValue.ToString());
	}
}
