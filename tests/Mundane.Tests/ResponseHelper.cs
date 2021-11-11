using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mundane.Tests;

[ExcludeFromCodeCoverage]
internal static class ResponseHelper
{
	internal static async ValueTask<string> Body(MundaneEngineResponse response)
	{
		await using (var memoryStream = new MemoryStream())
		{
			await response.WriteBodyToStream(memoryStream);

			memoryStream.Position = 0;

			using (var streamReader = new StreamReader(memoryStream, Encoding.UTF8))
			{
				return await streamReader.ReadToEndAsync();
			}
		}
	}
}
