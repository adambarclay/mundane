using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT;

[ExcludeFromCodeCoverage]
public static class ImplicitOperatorValidatedT_Returns_Correct_Value
{
	[Fact]
	public static void When_Value_Is_Null()
	{
		Validated<string> implicitValidatedT = (string)null!;

		Assert.Null(implicitValidatedT);
	}

	[Fact]
	public static void When_Value_Is_Valid()
	{
		var value = Guid.NewGuid().ToString();

		Validated<string> implicitValidatedT = value;

		Assert.Equal(value, implicitValidatedT);
	}

	[Fact]
	public static void When_Value_Is_Valid_When_Passed_With_String()
	{
		var value = Guid.NewGuid().ToString();

		Validated<string> implicitValidatedT = (value, string.Empty);

		Assert.Equal(value, implicitValidatedT);
	}
}
