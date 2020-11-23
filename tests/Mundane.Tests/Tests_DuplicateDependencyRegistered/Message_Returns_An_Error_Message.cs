using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_DuplicateDependencyRegistered
{
	[ExcludeFromCodeCoverage]
	public static class Message_Returns_An_Error_Message
	{
		[Fact]
		public static void Containing_The_Name_Of_The_Type_Passed_To_The_Constructor()
		{
			var exception = new DuplicateDependencyRegistered(typeof(Message_Returns_An_Error_Message));

			Assert.Equal(
				"The type \"Message_Returns_An_Error_Message\" has been registered more than once.",
				exception.Message);
		}
	}
}
