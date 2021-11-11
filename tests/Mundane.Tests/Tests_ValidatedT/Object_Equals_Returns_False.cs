using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT;

[ExcludeFromCodeCoverage]
public static class Object_Equals_Returns_False
{
	[Fact]
	public static void When_The_Other_Object_Is_Not_A_Validated_Object()
	{
		Validator.Validate(
			validator =>
			{
				var value = Guid.NewGuid().ToString();

				var first = validator.Value(value);

				var second = new
				{
					Value = value,
					ErrorMessages = new ReadOnlyCollection<string>(Array.Empty<string>())
				};

				Assert.False(first.Equals(second));

				return string.Empty;
			});
	}

	[Fact]
	public static void When_The_Other_Value_Is_Null()
	{
		Validator.Validate(
			validator =>
			{
				var first = validator.Value(Guid.NewGuid().ToString());
				var second = (Validated<string>?)null;

				Assert.False(first.Equals(second));

				return string.Empty;
			});
	}

	[Fact]
	public static void When_The_Values_Are_Different()
	{
		Validator.Validate(
			validator =>
			{
				var first = validator.Value(Guid.NewGuid().ToString());
				var second = validator.Value(Guid.NewGuid().ToString());

				Assert.False(first.Equals((object)second));

				return string.Empty;
			});
	}
}
