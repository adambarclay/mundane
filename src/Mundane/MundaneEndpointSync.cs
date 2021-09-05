namespace Mundane
{
	/// <summary>A synchronous endpoint delegate receiving the current request.</summary>
	/// <param name="request">The current request.</param>
	/// <returns>The endpoint response.</returns>
	public delegate Response MundaneEndpointSync(Request request);
}
