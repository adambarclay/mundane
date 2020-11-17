using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidationResult
{
	[ExcludeFromCodeCoverage]
	public static class Object_Equals_Returns_False
	{
		[Fact]
		public static void When_The_Other_Object_Is_Not_A_Validated_Object()
		{
			string value = Guid.NewGuid().ToString();

			var first = Validator.Validate(validator => value);

			var second = new
			{
				Invalid = false,
				Model = value
			};

			Assert.False(first.Equals(second));
		}

		[Fact]
		public static void When_The_Values_Are_Different()
		{
			var first = Validator.Validate(validator => Guid.NewGuid().ToString());
			var second = Validator.Validate(validator => Guid.NewGuid().ToString());

			Assert.False(first.Equals((object)second));
		}
	}
}
