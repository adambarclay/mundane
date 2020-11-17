using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class Object_Equals_Returns_False
	{
		[Fact]
		public static void When_The_Other_Object_Is_Not_A_Validated_Object()
		{
			string value = Guid.NewGuid().ToString();

			Validated<string> first = value;

			var second = new
			{
				Value = value,
				ErrorMessages = new ReadOnlyCollection<string>(Array.Empty<string>())
			};

			Assert.False(first.Equals(second));
		}

		[Fact]
		public static void When_The_Values_Are_Different()
		{
			Validated<string> first = Guid.NewGuid().ToString();
			Validated<string> second = Guid.NewGuid().ToString();

			Assert.False(first.Equals((object)second));
		}
	}
}
