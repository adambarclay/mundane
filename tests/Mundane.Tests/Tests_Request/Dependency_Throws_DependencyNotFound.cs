using System.Diagnostics.CodeAnalysis;
using Moq;
using Xunit;

namespace Mundane.Tests.Tests_Request
{
	[ExcludeFromCodeCoverage]
	public static class Dependency_Throws_DependencyNotFound
	{
		[Fact]
		public static void When_The_DependencyFinder_Returns_Null()
		{
			var dependencyFinder = new Mock<DependencyFinder>(MockBehavior.Strict);

			dependencyFinder.Setup(o => o.Find<TestDependency>(It.IsAny<Request>()!))!.Returns<Request>(_ => null!);

			var request = RequestHelper.Request(dependencyFinder.Object!);

			var exception = Assert.ThrowsAny<DependencyNotFound>(() => request.Dependency<TestDependency>());

			Assert.Equal("No concrete type has been registered for \"TestDependency\".", exception.Message);
		}

		private abstract class TestDependency
		{
		}
	}
}
