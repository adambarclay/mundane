using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_HeaderValue
{
	[ExcludeFromCodeCoverage]
	public static class Name_Returns_A_Value
	{
		[Fact]
		public static void Which_Was_Passed_To_The_Constructor()
		{
			var name = Guid.NewGuid().ToString();

			Assert.Equal(name, new HeaderValue(name, Guid.NewGuid().ToString()).Name);
		}
	}
}
