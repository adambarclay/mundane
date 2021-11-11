using System.Threading.Tasks;

namespace Mundane;

/// <summary>An asynchronous endpoint delegate receiving no parameters.</summary>
/// <returns>The endpoint response.</returns>
public delegate ValueTask<Response> MundaneEndpointNoParameters();
