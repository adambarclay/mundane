using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Dependencies
{
	[ExcludeFromCodeCoverage]
	public static class Find_Throws_DependencyNotFound
	{
		private interface TestDependencyBase
		{
		}

		[Fact]
		public static void When_The_Requested_Dependency_Has_Not_Been_Registered()
		{
			var dependencies = new Dependencies();

			var exception = Assert.ThrowsAny<DependencyNotFound>(
				() => dependencies.Find<TestDependencyBase>(RequestHelper.Request()));

			Assert.Equal("No concrete type has been registered for \"TestDependencyBase\".", exception.Message);
		}
	}
}
