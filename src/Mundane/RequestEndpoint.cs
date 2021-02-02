using System;
using System.ComponentModel;
using System.Linq;

namespace Mundane
{
	/// <summary>The endpoint for the request and the captured route parameters.</summary>
	public readonly struct RequestEndpoint : IEquatable<RequestEndpoint>
	{
		internal RequestEndpoint(MundaneEndpoint endpoint, EnumerableDictionary<string, string> routeParameters)
		{
			this.Endpoint = endpoint;
			this.RouteParameters = routeParameters;
		}

		/// <summary>Gets the endpoint delegate.</summary>
		public MundaneEndpoint Endpoint { get; }

		/// <summary>Gets the route parameters.</summary>
		public EnumerableDictionary<string, string> RouteParameters { get; }

		/// <summary>Equality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if equal, otherwise false.</returns>
		public static bool operator ==(RequestEndpoint left, RequestEndpoint right)
		{
			return left.Equals(right);
		}

		/// <summary>Inequality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if not equal, otherwise false.</returns>
		public static bool operator !=(RequestEndpoint left, RequestEndpoint right)
		{
			return !left.Equals(right);
		}

		/// <summary>Deconstructs a <see cref="RequestEndpoint"/>.</summary>
		/// <param name="endpoint">The endpoint delegate.</param>
		/// <param name="routeParameters">The route parameters.</param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(out MundaneEndpoint endpoint, out EnumerableDictionary<string, string> routeParameters)
		{
			endpoint = this.Endpoint;
			routeParameters = this.RouteParameters;
		}

		/// <inheritdoc/>
		public bool Equals(RequestEndpoint other)
		{
			return this.Endpoint == other.Endpoint &&
				this.RouteParameters.Dictionary.SequenceEqual(other.RouteParameters.Dictionary);
		}

		/// <inheritdoc/>
		public override bool Equals(object? obj)
		{
			return obj is RequestEndpoint other && this.Equals(other);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			HashCode hash = default;

			hash.Add(this.Endpoint.Method);

			foreach ((var key, var value) in this.RouteParameters)
			{
				hash.Add(key);
				hash.Add(value);
			}

			return hash.ToHashCode();
		}
	}
}
