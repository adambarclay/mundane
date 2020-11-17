using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_EnumerableDictionary
{
	[ExcludeFromCodeCoverage]
	public static class Implicit_Operator_Returns_A_Value
	{
		[Fact]
		public static void Containing_The_Same_Items_As_The_Value_Passed_To_It()
		{
			var expected = new Dictionary<string, string>
			{
				{ Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
				{ Guid.NewGuid().ToString(), Guid.NewGuid().ToString() },
				{ Guid.NewGuid().ToString(), Guid.NewGuid().ToString() }
			};

			var actual = (EnumerableDictionary<string, string>)expected;

			Assert.Equal<IEnumerable<KeyValuePair<string, string>>>(expected, actual);
		}
	}
}
