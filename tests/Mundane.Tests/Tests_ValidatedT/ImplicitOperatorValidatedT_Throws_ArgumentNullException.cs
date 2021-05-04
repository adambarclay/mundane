using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class ImplicitOperatorValidatedT_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_DisplayString_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
					Validated<string> implicitValidatedT = (string.Empty, null!);

					return implicitValidatedT;
				});

			Assert.Equal("displayString", exception.ParamName!);
		}

		[Fact]
		public static void When_Value_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() =>
				{
					Validated<string> implicitValidatedT = (null!, string.Empty);

					return implicitValidatedT;
				});

			Assert.Equal("value", exception.ParamName!);
		}
	}
}
