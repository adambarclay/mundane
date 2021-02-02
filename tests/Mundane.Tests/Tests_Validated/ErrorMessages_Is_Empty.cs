using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validated
{
	[ExcludeFromCodeCoverage]
	public static class ErrorMessages_Is_Empty
	{
		[Fact]
		public static void When_Initialised_With_A_String_And_No_Errors_Have_Been_Added()
		{
			Validated<string> value = Guid.NewGuid().ToString();

			Assert.Empty(value.ErrorMessages);
		}

		[Fact]
		public static void When_Initialised_With_A_Value_And_No_Errors_Have_Been_Added()
		{
			Validator.Validate(
				validator =>
				{
					var value = validator.Value(Guid.NewGuid().ToString());

					Assert.Empty(value.ErrorMessages);

					return value;
				});
		}
	}
}
