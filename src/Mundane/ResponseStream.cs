using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mundane;

/// <summary>The response stream.</summary>
[SuppressMessage(
	"Performance",
	"CA1815:Override equals and operator equals on value types",
	Justification = "It is unlikely that two of these will ever be compared.")]
public readonly struct ResponseStream
{
	internal ResponseStream(Request request, Stream stream)
	{
		this.Request = request;
		this.Stream = stream;
	}

	/// <summary>Gets the current request.</summary>
	public Request Request { get; }

	/// <summary>Gets the underlying response stream.</summary>
	public Stream Stream { get; }

	/// <summary>Flushes the response stream.</summary>
	/// <returns>A task that represents the asynchronous operation.</returns>
	public ValueTask Flush()
	{
		return new ValueTask(this.Stream.FlushAsync(this.Request.RequestAborted));
	}

	/// <summary>Writes a string to the response stream, encoding as UTF-8.</summary>
	/// <param name="value">The string to write.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
	public ValueTask Write(string value)
	{
		if (value is null)
		{
			return ValueTask.FromException(new ArgumentNullException(nameof(value)));
		}

		return this.Stream.WriteAsync(Encoding.UTF8.GetBytes(value), this.Request.RequestAborted);
	}

	/// <summary>Writes a byte array to the response stream.</summary>
	/// <param name="value">The byte array to write.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
	public ValueTask Write(byte[] value)
	{
		if (value is null)
		{
			return ValueTask.FromException(new ArgumentNullException(nameof(value)));
		}

		return this.Stream.WriteAsync(value, this.Request.RequestAborted);
	}

	/// <summary>Writes a stream to the response stream.</summary>
	/// <param name="value">The stream to write.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
	public ValueTask Write(Stream value)
	{
		if (value is null)
		{
			return ValueTask.FromException(new ArgumentNullException(nameof(value)));
		}

		return new ValueTask(value.CopyToAsync(this.Stream, this.Request.RequestAborted));
	}
}
