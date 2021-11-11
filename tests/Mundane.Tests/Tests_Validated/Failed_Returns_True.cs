using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validated;

[ExcludeFromCodeCoverage]
public static class Failed_Returns_True
{
	[Fact]
	public static void When_Initialised_With_A_String_And_At_Least_One_Error_Has_Been_Added()
	{
		Validated<string> value = Guid.NewGuid().ToString();

		value.AddErrorMessage("Error");

		Assert.True(value.Failed);
	}

	[Fact]
	public static void When_Initialised_With_A_Value_And_At_Least_One_Error_Has_Been_Added()
	{
		Validator.Validate(
			validator =>
			{
				var value = validator.Value(Guid.NewGuid().ToString());

				value.AddErrorMessage("Error");

				Assert.True(value.Failed);

				return value;
			});
	}
}
