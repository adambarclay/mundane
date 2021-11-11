using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Dependencies;

[ExcludeFromCodeCoverage]
public static class Find_Throws_ArgumentNullException
{
	private interface TestDependencyBase
	{
	}

	[Fact]
	public static void When_The_Request_Parameter_Is_Null()
	{
		var dependencies = new Dependencies(new Dependency<TestDependencyBase>(() => new TestDependency()));

		var exception = Assert.ThrowsAny<ArgumentNullException>(() => dependencies.Find<TestDependencyBase>(null!));

		Assert.Equal("request", exception.ParamName!);
	}

	private sealed class TestDependency : TestDependencyBase
	{
	}
}
