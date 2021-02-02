using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_ValidatedT
{
	[ExcludeFromCodeCoverage]
	public static class Object_Equals_Returns_True
	{
		[Fact]
		public static void When_The_Values_Are_The_Same()
		{
			Validator.Validate(
				validator =>
				{
					var value = Guid.NewGuid().ToString();

					var first = validator.Value(value);
					var second = validator.Value(value);

					Assert.True(first.Equals((object)second));

					return string.Empty;
				});
		}
	}
}
