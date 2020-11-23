using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_DependencyNotFound
{
	[ExcludeFromCodeCoverage]
	public static class Message_Returns_An_Error_Message
	{
		[Fact]
		public static void Containing_The_Name_Of_The_Type_Passed_To_The_Constructor()
		{
			var exception = new DependencyNotFound(typeof(Message_Returns_An_Error_Message));

			Assert.Equal(
				"No concrete type has been registered for \"Message_Returns_An_Error_Message\".",
				exception.Message);
		}
	}
}
