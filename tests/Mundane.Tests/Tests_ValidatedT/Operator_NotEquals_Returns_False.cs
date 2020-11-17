using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class Operator_NotEquals_Returns_False
	{
		[Fact]
		public static void When_The_Values_Are_The_Same()
		{
			var value = Guid.NewGuid().ToString();

			Validated<string> first = value;
			Validated<string> second = value;

			Assert.False(first != second);
		}
	}
}