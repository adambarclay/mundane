using System.Threading.Tasks;

namespace Mundane
{
	/// <summary>An asynchronous endpoint delegate receiving the current request.</summary>
	/// <param name="request">The current request.</param>
	/// <returns>The endpoint response.</returns>
	public delegate ValueTask<Response> MundaneEndpoint(Request request);
}
