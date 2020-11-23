using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_DuplicateDependencyRegistered
{
	[ExcludeFromCodeCoverage]
	public static class Constructor_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_DependencyType_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => new DuplicateDependencyRegistered(null!));

			Assert.Equal("dependencyType", exception.ParamName);
		}
	}
}
