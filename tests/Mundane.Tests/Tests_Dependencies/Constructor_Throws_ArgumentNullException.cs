using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Dependencies;

[ExcludeFromCodeCoverage]
public static class Constructor_Throws_ArgumentNullException
{
	[Fact]
	public static void When_Any_Of_The_Items_In_The_Dependencies_Array_Parameter_Is_Null()
	{
		Dependency[] dependencies = { null! };

		var exception = Assert.ThrowsAny<ArgumentNullException>(() => new Dependencies(dependencies));

		Assert.Equal("dependencies", exception.ParamName!);
	}

	[Fact]
	public static void When_The_Dependencies_Array_Parameter_Is_Null()
	{
		var exception = Assert.ThrowsAny<ArgumentNullException>(() => new Dependencies(null!));

		Assert.Equal("dependencies", exception.ParamName!);
	}
}
