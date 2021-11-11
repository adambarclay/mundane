using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT;

[ExcludeFromCodeCoverage]
public static class GetHashCode_Returns_The_Same_Value_For_Two_Objects
{
	[Fact]
	public static void When_The_Values_Are_The_Same()
	{
		Validator.Validate(
			validator =>
			{
				var value = Guid.NewGuid().ToString();

				var first = validator.Value(value);
				var second = validator.Value(value);

				Assert.Equal(first.GetHashCode(), second.GetHashCode());

				return string.Empty;
			});
	}
}
