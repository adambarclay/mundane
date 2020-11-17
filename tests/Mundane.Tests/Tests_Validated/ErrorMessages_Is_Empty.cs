using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validated
{
	[ExcludeFromCodeCoverage]
	public static class ErrorMessages_Is_Empty
	{
		[Fact]
		public static void When_No_Errors_Have_Been_Added()
		{
			Validated<string> validatedValue = Guid.NewGuid().ToString();

			Assert.Empty(validatedValue.ErrorMessages);
		}
	}
}
