using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Response;

[ExcludeFromCodeCoverage]
public static class File_Throws_ArgumentNullException
{
	[Fact]
	public static void When_The_BodyWriter_Parameter_Is_Null()
	{
		var exception1 = Assert.ThrowsAny<ArgumentNullException>(() => Response.File(null!, Guid.NewGuid().ToString()));

		Assert.Equal("bodyWriter", exception1.ParamName!);

		var exception2 = Assert.ThrowsAny<ArgumentNullException>(
			() => Response.File(null!, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

		Assert.Equal("bodyWriter", exception2.ParamName!);
	}

	[Fact]
	public static void When_The_ContentType_Parameter_Is_Null()
	{
		var exception1 = Assert.ThrowsAny<ArgumentNullException>(
			() => Response.File(_ => ValueTask.CompletedTask, null!));

		Assert.Equal("contentType", exception1.ParamName!);

		var exception2 = Assert.ThrowsAny<ArgumentNullException>(
			() => Response.File(_ => ValueTask.CompletedTask, null!, Guid.NewGuid().ToString()));

		Assert.Equal("contentType", exception2.ParamName!);
	}

	[Fact]
	public static void When_The_FileName_Parameter_Is_Null()
	{
		var exception = Assert.ThrowsAny<ArgumentNullException>(
			() => Response.File(_ => ValueTask.CompletedTask, Guid.NewGuid().ToString(), null!));

		Assert.Equal("fileName", exception.ParamName!);
	}
}
