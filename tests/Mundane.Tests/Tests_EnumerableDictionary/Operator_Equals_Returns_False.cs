using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_EnumerableDictionary
{
	[ExcludeFromCodeCoverage]
	public static class Operator_Equals_Returns_False
	{
		[Fact]
		public static void When_The_Dictionaries_Are_Different()
		{
			var first = new EnumerableDictionary<string, string>(new Dictionary<string, string>());
			var second = new EnumerableDictionary<string, string>(new Dictionary<string, string>());

			Assert.False(first == second);
		}
	}
}
