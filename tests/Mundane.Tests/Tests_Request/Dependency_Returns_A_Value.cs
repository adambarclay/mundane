using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Mundane.Tests.Tests_Request
{
	[ExcludeFromCodeCoverage]
	public static class Dependency_Returns_A_Value
	{
		private interface TestDependencyBase
		{
		}

		[Fact]
		public static void Returned_By_The_Dependency_Finder()
		{
			var dependencyFinder = new Mock<DependencyFinder>(MockBehavior.Strict);

			dependencyFinder.Setup(o => o.Find<TestDependencyBase>(It.IsAny<Request>()!))!.Returns<Request>(
				r => new TestDependencyWithRequest(r));

			var request = RequestHelper.Request(dependencyFinder.Object!);

			var result = request.Dependency<TestDependencyBase>();

			Assert.IsType<TestDependencyWithRequest>(result);

			var dependency = (TestDependencyWithRequest)result;

			Assert.Same(request, dependency.Request);
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
}
