using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Mundane
{
	/// <summary>An asynchronous endpoint delegate receiving the current request.</summary>
	/// <param name="request">The current request.</param>
	/// <returns>The endpoint response.</returns>
	public delegate ValueTask<Response> MundaneEndpointDelegate([DisallowNull] Request request);

	/// <summary>An asynchronous endpoint delegate receiving no parameters.</summary>
	/// <returns>The endpoint response.</returns>
	public delegate ValueTask<Response> MundaneEndpointDelegateNoParameters();

	/// <summary>A synchronous endpoint delegate receiving the current request.</summary>
	/// <param name="request">The current request.</param>
	/// <returns>The endpoint response.</returns>
	[return: NotNull]
	public delegate Response MundaneEndpointDelegateSync([DisallowNull] Request request);

	/// <summary>A synchronous endpoint delegate receiving no parameters.</summary>
	/// <returns>The endpoint response.</returns>
	[return: NotNull]
	public delegate Response MundaneEndpointDelegateNoParametersSync();

	/// <summary>Converts all of the possible endpoints to <see cref="MundaneEndpointDelegate"/> which the engine requires.</summary>
	public static class MundaneEndpoint
	{
		/// <summary>Creates a Mundane endpoint delegate from a synchronous delegate which receives no parameters.</summary>
		/// <param name="endpoint">The endpoint delegate.</param>
		/// <returns>A Mundane endpoint delegate.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="endpoint"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static MundaneEndpointDelegate Create([DisallowNull] MundaneEndpointDelegateNoParametersSync endpoint)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException(nameof(endpoint));
			}

			return request =>
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
		public static MundaneEndpointDelegate Create([DisallowNull] MundaneEndpointDelegateSync endpoint)
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
		public static MundaneEndpointDelegate Create([DisallowNull] MundaneEndpointDelegateNoParameters endpoint)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException(nameof(endpoint));
			}

			return request => endpoint();
		}

		/// <summary>Creates a Mundane endpoint delegate from an asynchronous delegate which receives the current request.</summary>
		/// <param name="endpoint">The endpoint delegate.</param>
		/// <returns>A Mundane endpoint delegate.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="endpoint"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static MundaneEndpointDelegate Create([DisallowNull] MundaneEndpointDelegate endpoint)
		{
			if (endpoint == null)
			{
				throw new ArgumentNullException(nameof(endpoint));
			}

			return endpoint;
		}
	}
}
