using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Dependencies;

[ExcludeFromCodeCoverage]
public static class Constructor_Throws_DuplicateDependencyRegistered
{
	private interface TestDependencyBase
	{
	}

	[Fact]
	public static void When_More_Than_One_Dependency_Is_Registered_For_The_Same_Type()
	{
		var exception = Assert.ThrowsAny<DuplicateDependencyRegistered>(
			() => new Dependencies(
				new Dependency<TestDependencyBase>(() => new TestDependencyFirst()),
				new Dependency<TestDependencyBase>(() => new TestDependencySecond())));

		Assert.Equal("The type \"TestDependencyBase\" has been registered more than once.", exception.Message);
	}

	private sealed class TestDependencyFirst : TestDependencyBase
	{
	}

	private sealed class TestDependencySecond : TestDependencyBase
	{
	}
}
