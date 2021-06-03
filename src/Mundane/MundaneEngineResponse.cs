using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mundane
{
	/// <summary>The response produced by the Mundane engine.</summary>
	public readonly struct MundaneEngineResponse : IEquatable<MundaneEngineResponse>
	{
		private readonly BodyWriter bodyWriter;
		private readonly Request request;

		internal MundaneEngineResponse(
			Request request,
			int statusCode,
			List<HeaderValue> headers,
			BodyWriter bodyWriter)
		{
			this.request = request;
			this.StatusCode = statusCode;
			this.Headers = new EnumerableCollection<HeaderValue>(headers);
			this.bodyWriter = bodyWriter;
		}

		/// <summary>Gets the HTTP response headers.</summary>
		public EnumerableCollection<HeaderValue> Headers { get; }

		/// <summary>Gets the HTTP response status code.</summary>
		public int StatusCode { get; }

		/// <summary>Equality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if equal, otherwise false.</returns>
		public static bool operator ==(MundaneEngineResponse left, MundaneEngineResponse right)
		{
			return left.Equals(right);
		}

		/// <summary>Inequality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if not equal, otherwise false.</returns>
		public static bool operator !=(MundaneEngineResponse left, MundaneEngineResponse right)
		{
			return !left.Equals(right);
		}

		/// <inheritdoc/>
		public bool Equals(MundaneEngineResponse other)
		{
			return this.request == other.request &&
				this.StatusCode == other.StatusCode &&
				this.Headers.SequenceEqual(other.Headers) &&
				this.bodyWriter == other.bodyWriter;
		}

		/// <inheritdoc/>
		public override bool Equals(object? obj)
		{
			return obj is MundaneEngineResponse other && this.Equals(other);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			HashCode hash = default;

			hash.Add(this.request);
			hash.Add(this.StatusCode);
			hash.Add(this.bodyWriter.Method);

			foreach (var header in this.Headers)
			{
				hash.Add(header);
			}

			return hash.ToHashCode();
		}

		/// <summary>Writes the response body to the specified stream.</summary>
		/// <param name="stream">The response stream.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/>.</exception>
		public ValueTask WriteBodyToStream(Stream stream)
		{
			if (stream is null)
			{
				return ValueTask.FromException(new ArgumentNullException(nameof(stream)));
			}

			return this.bodyWriter.Invoke(new ResponseStream(this.request, stream));
		}
	}
}
