using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_ResponseStream
{
	[ExcludeFromCodeCoverage]
	public static class The_Underlying_Stream_Gets_Flushed
	{
		[Fact]
		public static async Task When_Flush_Is_Called()
		{
			await using (var stream = new FakeStream())
			{
				var response = await MundaneEngine.ExecuteRequest(
					MundaneEndpointFactory.Create(() => Response.Ok(o => o.Flush())),
					RequestHelper.Request());

				await response.WriteBodyToStream(stream);

				Assert.True(stream.Flushed);
			}
		}

		private sealed class FakeStream : Stream
		{
			internal FakeStream()
			{
				this.CanRead = true;
				this.CanSeek = true;
				this.CanWrite = true;
				this.Length = 0;
			}

			public override bool CanRead { get; }

			public override bool CanSeek { get; }

			public override bool CanWrite { get; }

			public override long Length { get; }

			public override long Position { get; set; }

			internal bool Flushed { get; private set; }

			public override void Flush()
			{
				this.Flushed = true;
			}

			public override int Read(byte[] buffer, int offset, int count)
			{
				return 0;
			}

			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0;
			}

			public override void SetLength(long value)
			{
			}

			public override void Write(byte[] buffer, int offset, int count)
			{
			}
		}
	}
}
