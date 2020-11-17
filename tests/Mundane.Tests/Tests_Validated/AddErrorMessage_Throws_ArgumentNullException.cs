using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validated
{
	[ExcludeFromCodeCoverage]
	public static class AddErrorMessage_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_ErrorMessage_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => Validator.Validate(
					validator =>
					{
						var value = validator.Value(Guid.NewGuid().ToString());

						value.AddErrorMessage(null!);

						return value;
					}));

			Assert.Equal("errorMessage", exception.ParamName);
		}
	}
}
