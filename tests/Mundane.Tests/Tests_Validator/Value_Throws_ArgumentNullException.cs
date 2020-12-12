using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Validator
{
	[ExcludeFromCodeCoverage]
	public static class Value_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Value_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => Validator.Validate(validator => validator.Value((object)null!)));

			Assert.Equal("value", exception.ParamName!);
		}
	}
}
