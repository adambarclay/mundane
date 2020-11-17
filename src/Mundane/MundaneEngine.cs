using System;
using System.Diagnostics.CodeAnalysis;
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
		/// <exception cref="EndpointReturnedNull">The endpoint returns a <see langword="null"/> <see cref="Task"/> or the <see cref="Task"/> returns a <see langword="null"/> <see cref="Response"/>.</exception>
		[return: NotNull]
		public static async Task<MundaneEngineResponse> ExecuteRequest(
			[DisallowNull] MundaneEndpointDelegate endpoint,
			[DisallowNull] Request request)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException(nameof(endpoint));
			}

			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			var responseTask = endpoint.Invoke(request);

			if (responseTask == null)
			{
				throw new EndpointReturnedNull("The endpoint returned a null Task<Response>.");
			}

			Response response;

			if ((response = await responseTask) == null)
			{
				throw new EndpointReturnedNull("The endpoint returned a null Response.");
			}

			return request.Method != HttpMethod.Head
				? response.BuildResponse(request)
				: response.BuildResponseWithNoBody(request);
		}
	}
}
