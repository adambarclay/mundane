using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validated
{
	[ExcludeFromCodeCoverage]
	public static class Failed_Returns_True
	{
		[Fact]
		public static void When_At_Least_One_Error_Has_Been_Added()
		{
			Validated<string> validatedValue = Guid.NewGuid().ToString();

			validatedValue.AddErrorMessage("Error");

			Assert.True(validatedValue.Failed);
		}
	}
}
