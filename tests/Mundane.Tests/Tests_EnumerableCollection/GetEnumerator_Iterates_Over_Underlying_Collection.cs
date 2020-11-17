using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_EnumerableCollection
{
	[ExcludeFromCodeCoverage]
	public static class GetEnumerator_Iterates_Over_Underlying_Collection
	{
		[Fact]
		public static void When_Called_As_Generic_IEnumerable()
		{
			var collection = new List<string>
			{
				Guid.NewGuid().ToString(),
				Guid.NewGuid().ToString(),
				Guid.NewGuid().ToString()
			};

			var count = 0;

			var enumerable = (IEnumerable<string>)new EnumerableCollection<string>(collection);

			foreach (var item in enumerable)
			{
				Assert.Equal(collection[count++], item);
			}
		}

		[Fact]
		public static void When_Called_As_IEnumerable()
		{
			var collection = new List<string>
			{
				Guid.NewGuid().ToString(),
				Guid.NewGuid().ToString(),
				Guid.NewGuid().ToString()
			};

			var count = 0;

			var enumerable = (IEnumerable)new EnumerableCollection<string>(collection);

			foreach (var item in enumerable)
			{
				Assert.Equal(collection[count++], item);
			}
		}

		[Fact]
		public static void When_Called_Directly()
		{
			var collection = new List<string>
			{
				Guid.NewGuid().ToString(),
				Guid.NewGuid().ToString(),
				Guid.NewGuid().ToString()
			};

			var count = 0;

			var enumerable = new EnumerableCollection<string>(collection);

			foreach (var item in enumerable)
			{
				Assert.Equal(collection[count++], item);
			}
		}
	}
}
