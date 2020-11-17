using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_RequestHost
{
	[ExcludeFromCodeCoverage]
	public static class HostName_Returns_A_Value
	{
		[Fact]
		public static void Which_Was_Passed_To_The_Constructor()
		{
			var hostName = Guid.NewGuid().ToString();

			Assert.Equal(hostName, new RequestHost("scheme", hostName, "pathBase").HostName);
		}
	}
}
