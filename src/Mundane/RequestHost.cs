using System;
using System.Diagnostics.CodeAnalysis;

namespace Mundane
{
	/// <summary>The host of the request.</summary>
	public sealed class RequestHost
	{
		/// <summary>Initializes a new instance of the <see cref="RequestHost"/> class.</summary>
		/// <param name="scheme">The request scheme.</param>
		/// <param name="hostName">The name of the application host.</param>
		/// <param name="pathBase">The base path the application is hosted under.</param>
		/// <exception cref="ArgumentNullException"><paramref name="scheme"/>, <paramref name="hostName"/> or <paramref name="pathBase"/> is <see langword="null"/>.</exception>
		public RequestHost([DisallowNull] string scheme, [DisallowNull] string hostName, [DisallowNull] string pathBase)
		{
			if (scheme == null)
			{
				throw new ArgumentNullException(nameof(scheme));
			}

			if (hostName == null)
			{
				throw new ArgumentNullException(nameof(hostName));
			}

			if (pathBase == null)
			{
				throw new ArgumentNullException(nameof(pathBase));
			}

			this.Scheme = scheme;
			this.HostName = hostName;
			this.PathBase = pathBase;
		}

		/// <summary>Gets the name of the application host. e.g. "www.example.com".</summary>
		[NotNull]
		public string HostName { get; }

		/// <summary>Gets the base path the application is hosted under, e.g. if your application is hosted under "www.example.com/my-app", <see cref="PathBase"/> will be "my-app".</summary>
		[NotNull]
		public string PathBase { get; }

		/// <summary>Gets the request scheme. e.g. "https".</summary>
		[NotNull]
		public string Scheme { get; }
	}
}
