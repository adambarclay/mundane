using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_DependencyT
{
	[ExcludeFromCodeCoverage]
	public static class Constructor_Throws_ArgumentNullException
	{
		[Fact]
		public static void When_The_Dependency_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(() => new Dependency<object>((object)null!));

			Assert.Equal("dependency", exception.ParamName!);
		}

		[Fact]
		public static void When_The_No_Parameters_CreateDependency_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => new Dependency<object>((null as CreateDependencyNoParameters<object>)!));

			Assert.Equal("createDependency", exception.ParamName!);
		}

		[Fact]
		public static void When_The_Request_CreateDependency_Parameter_Is_Null()
		{
			var exception = Assert.ThrowsAny<ArgumentNullException>(
				() => new Dependency<object>((null as CreateDependency<object>)!));

			Assert.Equal("createDependency", exception.ParamName!);
		}
	}
}
