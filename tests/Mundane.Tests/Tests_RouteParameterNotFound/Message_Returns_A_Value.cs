using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_RouteParameterNotFound;

[ExcludeFromCodeCoverage]
public static class Message_Returns_A_Value
{
	[Fact]
	public static void Containing_The_Parameter_Name()
	{
		var parameterName = Guid.NewGuid().ToString();

		var exception = new RouteParameterNotFound(parameterName);

		Assert.Equal(
			$"The route configuration does not contain a parameter called \"{parameterName}\".",
			exception.Message);
	}
}
