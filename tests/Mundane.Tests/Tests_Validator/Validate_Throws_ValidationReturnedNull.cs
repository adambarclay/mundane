using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validator
{
	[ExcludeFromCodeCoverage]
	public static class Validate_Throws_ValidationReturnedNull
	{
		[Fact]
		public static void When_The_Validation_Returns_Null()
		{
			var exception = Assert.ThrowsAny<ValidationReturnedNull>(() => Validator.Validate(_ => (object)null!));

			Assert.Equal("The validation returned null.", exception.Message);
		}
	}
}
