using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Validator
{
	[ExcludeFromCodeCoverage]
	public static class Validate_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Action_Parameter_Is_Null()
		{
			var exception =
				Assert.ThrowsAny<ArgumentNullException>(() => Validator.Validate((null as ValidatorDelegate<object>)!));

			Assert.Equal("validate", exception.ParamName!);
		}

		[Fact]
		public static async Task When_The_Async_Action_Parameter_Is_Null()
		{
			var exception = await Assert.ThrowsAnyAsync<ArgumentNullException>(
				async () => await Validator.Validate((null as ValidatorDelegate<ValueTask<object>>)!));

			Assert.Equal("validate", exception.ParamName!);
		}
	}
}
