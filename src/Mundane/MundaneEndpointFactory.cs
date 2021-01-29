using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Mundane
{
	/// <summary>An asynchronous endpoint delegate receiving the current request.</summary>
	/// <param name="request">The current request.</param>
	/// <returns>The endpoint response.</returns>
	public delegate ValueTask<Response> MundaneEndpoint([DisallowNull] Request request);

	/// <summary>An asynchronous endpoint delegate receiving no parameters.</summary>
	/// <returns>The endpoint response.</returns>
	public delegate ValueTask<Response> MundaneEndpointNoParameters();

	/// <summary>A synchronous endpoint delegate receiving the current request.</summary>
	/// <param name="request">The current request.</param>
	/// <returns>The endpoint response.</returns>
	[return: NotNull]
	public delegate Response MundaneEndpointSync([DisallowNull] Request request);

	/// <summary>A synchronous endpoint delegate receiving no parameters.</summary>
	/// <returns>The endpoint response.</returns>
	[return: NotNull]
	public delegate Response MundaneEndpointNoParametersSync();

	/// <summary>Converts all of the possible endpoints to <see cref="MundaneEndpoint"/> which the engine requires.</summary>
	public static class MundaneEndpointFactory
	{
		/// <summary>Creates a Mundane endpoint delegate from a synchronous delegate which receives no parameters.</summary>
		/// <param name="endpoint">The endpoint delegate.</param>
		/// <returns>A Mundane endpoint delegate.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="endpoint"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static MundaneEndpoint Create([DisallowNull] MundaneEndpointNoParametersSync endpoint)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException(nameof(endpoint));
			}

			return _ =>
			{
				try
				{
					return ValueTask.FromResult(endpoint.Invoke());
				}
				catch (Exception exception)
				{
					return ValueTask.FromException<Response>(exception);
				}
			};
		}

		/// <summary>Creates a Mundane endpoint delegate from a synchronous delegate which receives the current request.</summary>
		/// <param name="endpoint">The endpoint delegate.</param>
		/// <returns>A Mundane endpoint delegate.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="endpoint"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static MundaneEndpoint Create([DisallowNull] MundaneEndpointSync endpoint)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException(nameof(endpoint));
			}

			return request =>
			{
				try
				{
					return ValueTask.FromResult(endpoint.Invoke(request));
				}
				catch (Exception exception)
				{
					return ValueTask.FromException<Response>(exception);
				}
			};
		}

		/// <summary>Creates a Mundane endpoint delegate from an asynchronous delegate which receives no parameters.</summary>
		/// <param name="endpoint">The endpoint delegate.</param>
		/// <returns>A Mundane endpoint delegate.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="endpoint"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static MundaneEndpoint Create([DisallowNull] MundaneEndpointNoParameters endpoint)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException(nameof(endpoint));
			}

			return _ => endpoint();
		}

		/// <summary>Creates a Mundane endpoint delegate from an asynchronous delegate which receives the current request.</summary>
		/// <param name="endpoint">The endpoint delegate.</param>
		/// <returns>A Mundane endpoint delegate.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="endpoint"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static MundaneEndpoint Create([DisallowNull] MundaneEndpoint endpoint)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException(nameof(endpoint));
			}

			return endpoint;
		}
	}
}
