using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validator;

[ExcludeFromCodeCoverage]
public static class Value_Throws_ArgumentNullException
{
	[Fact]
	public static void When_The_Convert_Out_Convert_Parameter_Is_Null()
	{
		var exception = Assert.ThrowsAny<ArgumentNullException>(
			() => Validator.Validate(
				validator => validator.Value(Guid.NewGuid().ToString(), (ValidateConvertOut<int>)null!, 0, "Error")));

		Assert.Equal("convert", exception.ParamName!);
	}

	[Fact]
	public static void When_The_Convert_Out_Value_Parameter_Is_Null()
	{
		var exception = Assert.ThrowsAny<ArgumentNullException>(
			() => Validator.Validate(validator => validator.Value(null!, int.TryParse, 0, "Error")));

		Assert.Equal("value", exception.ParamName!);
	}

	[Fact]
	public static void When_The_Convert_Return_Convert_Parameter_Is_Null()
	{
		var exception = Assert.ThrowsAny<ArgumentNullException>(
			() => Validator.Validate(
				validator => validator.Value(
					Guid.NewGuid().ToString(),
					(ValidateConvertReturn<int>)null!,
					0,
					"Error")));

		Assert.Equal("convert", exception.ParamName!);
	}

	[Fact]
	public static void When_The_Convert_Return_Value_Parameter_Is_Null()
	{
		var exception = Assert.ThrowsAny<ArgumentNullException>(
			() => Validator.Validate(validator => validator.Value(null!, int.Parse, 0, "Error")));

		Assert.Equal("value", exception.ParamName!);
	}

	[Fact]
	public static void When_The_Value_Parameter_Is_Null()
	{
		var exception = Assert.ThrowsAny<ArgumentNullException>(
			() => Validator.Validate(validator => validator.Value((object)null!)));

		Assert.Equal("value", exception.ParamName!);
	}
}
