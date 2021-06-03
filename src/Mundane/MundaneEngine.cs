using System;
using System.Threading.Tasks;

namespace Mundane
{
	/// <summary>Executes a request using the Mundane framework.</summary>
	public static class MundaneEngine
	{
		/// <summary>Executes a request.</summary>
		/// <param name="endpoint">The endpoint to invoke.</param>
		/// <param name="request">The HTTP request.</param>
		/// <returns>The response returned by the endpoint.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="endpoint"/> or <paramref name="request"/> is <see langword="null"/>.</exception>
		/// <exception cref="EndpointReturnedNull">The endpoint returns a <see langword="null"/> <see cref="Response"/>.</exception>
		public static async ValueTask<MundaneEngineResponse> ExecuteRequest(MundaneEndpoint endpoint, Request request)
		{
			if (endpoint is null)
			{
				throw new ArgumentNullException(nameof(endpoint));
			}

			if (request is null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			Response response;

			if ((response = await endpoint.Invoke(request)) is null)
			{
				throw new EndpointReturnedNull("The endpoint returned a null Response.");
			}

			return request.Method != HttpMethod.Head
				? response.BuildResponse(request)
				: response.BuildResponseWithNoBody(request);
		}
	}
}
