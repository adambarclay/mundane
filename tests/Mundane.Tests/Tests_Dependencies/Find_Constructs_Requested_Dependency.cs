using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Dependencies;

[ExcludeFromCodeCoverage]
public static class Find_Constructs_Requested_Dependency
{
	private interface TestDependencyBase
	{
	}

	[Fact]
	public static void When_The_Dependency_Is_Registered_With_A_Delegate_Taking_No_Parameters()
	{
		var dependencies = new Dependencies(new Dependency<TestDependencyBase>(() => new TestDependency()));

		var instance = dependencies.Find<TestDependencyBase>(RequestHelper.Request());

		Assert.IsType<TestDependency>(instance);
	}

	[Fact]
	public static void When_The_Dependency_Is_Registered_With_A_Delegate_Taking_Request()
	{
		var dependencies = new Dependencies(new Dependency<TestDependencyBase>(r => new TestDependencyWithRequest(r)));

		var request = RequestHelper.Request();

		var instance = dependencies.Find<TestDependencyBase>(request);

		Assert.IsType<TestDependencyWithRequest>(instance);
		Assert.Same(request, ((TestDependencyWithRequest)instance).Request);
	}

	[Fact]
	public static void When_The_Dependency_Is_Registered_With_An_Object_Instance()
	{
		var dependencies = new Dependencies(new Dependency<TestDependencyBase>(new TestDependency()));

		var instance = dependencies.Find<TestDependencyBase>(RequestHelper.Request());

		Assert.IsType<TestDependency>(instance);
	}

	private sealed class TestDependency : TestDependencyBase
	{
	}

	private sealed class TestDependencyWithRequest : TestDependencyBase
	{
		internal TestDependencyWithRequest(Request request)
		{
			this.Request = request;
		}

		internal Request Request { get; }
	}
}
