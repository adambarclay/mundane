using System;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	public static class Value_Throws_InvalidOperationException
	{
		[Fact]
		public static void When_The_Value_Has_Not_Been_Initialised()
		{
			Validated<string> uninitialisedValue = "Some display string";

			var exception = Assert.ThrowsAny<InvalidOperationException>(() => (string)uninitialisedValue);

			Assert.Equal("A validated value has not been set.", exception.Message);
		}
	}
}
