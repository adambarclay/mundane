using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class Operator_Equals_Returns_False
	{
		[Fact]
		public static void When_The_Values_Are_Different()
		{
			Validated<string> first = Guid.NewGuid().ToString();
			Validated<string> second = Guid.NewGuid().ToString();

			Assert.False(first == second);
		}
	}
}
