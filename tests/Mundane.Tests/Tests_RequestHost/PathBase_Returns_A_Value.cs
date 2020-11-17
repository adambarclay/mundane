using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_RequestHost
{
	[ExcludeFromCodeCoverage]
	public static class PathBase_Returns_A_Value
	{
		[Fact]
		public static void Which_Was_Passed_To_The_Constructor()
		{
			var pathBase = Guid.NewGuid().ToString();

			Assert.Equal(pathBase, new RequestHost("scheme", "hostName", pathBase).PathBase);
		}
	}
}
