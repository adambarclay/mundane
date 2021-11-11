using System.Threading.Tasks;

namespace Mundane;

/// <summary>Writes the response body to the output stream.</summary>
/// <param name="responseStream">The response output stream.</param>
public delegate ValueTask BodyWriter(ResponseStream responseStream);
