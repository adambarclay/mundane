using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validated
{
	[ExcludeFromCodeCoverage]
	public static class ErrorMessages_Returns_All_Errors
	{
		[Fact]
		public static void When_Errors_Have_Been_Added()
		{
			var errors = new[] { "Error 1", "Error 2" };

			Validated<string> validatedValue = Guid.NewGuid().ToString();

			validatedValue.AddErrorMessage(errors[0]);
			validatedValue.AddErrorMessage(errors[1]);

			Assert.Equal(errors, validatedValue.ErrorMessages);
		}
	}
}
