using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;

namespace Mundane;

/// <summary>An HTTP header value.</summary>
public readonly struct HeaderValue : IEquatable<HeaderValue>
{
	private const string LastModifiedFormat = "ddd, dd MM yyyy HH:mm:ss 'GMT'";

	/// <summary> Initializes a new instance of the <see cref="HeaderValue"/> struct.</summary>
	/// <param name="name">The name of the header.</param>
	/// <param name="value">The value of the header.</param>
	/// <exception cref="ArgumentNullException"><paramref name="name"/> or <paramref name="value"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="name"/> does not have a value.</exception>
	public HeaderValue(string name, string value)
	{
		if (name is null)
		{
			throw new ArgumentNullException(nameof(name));
		}

		if (value is null)
		{
			throw new ArgumentNullException(nameof(value));
		}

		if (name.AsSpan().Trim().IsEmpty)
		{
			throw new ArgumentException("Header name must have a value.", nameof(name));
		}

		this.Name = name;
		this.Value = value;
	}

	/// <summary>Gets the name of the header.</summary>
	public string Name { get; }

	/// <summary>Gets the value of the header.</summary>
	public string Value { get; }

	/// <summary>Equality operator.</summary>
	/// <param name="left">The left side of the operation.</param>
	/// <param name="right">The right side of the operation.</param>
	/// <returns>true if equal, otherwise false.</returns>
	public static bool operator ==(HeaderValue left, HeaderValue right)
	{
		return left.Equals(right);
	}

	/// <summary>Inequality operator.</summary>
	/// <param name="left">The left side of the operation.</param>
	/// <param name="right">The right side of the operation.</param>
	/// <returns>true if not equal, otherwise false.</returns>
	public static bool operator !=(HeaderValue left, HeaderValue right)
	{
		return !left.Equals(right);
	}

	/// <summary>Creates the "allow" header value.</summary>
	/// <param name="allowedMethods">The allowed HTTP methods.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="allowedMethods"/> is <see langword="null"/>.</exception>
	public static HeaderValue Allow(IEnumerable<string> allowedMethods)
	{
		if (allowedMethods is null)
		{
			throw new ArgumentNullException(nameof(allowedMethods));
		}

		return new HeaderValue("allow", string.Join(",", allowedMethods));
	}

	/// <summary>Creates the "cache-control" header value.</summary>
	/// <param name="directives">The cache control directives.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="directives"/> is <see langword="null"/>.</exception>
	public static HeaderValue CacheControl(string directives)
	{
		if (directives is null)
		{
			throw new ArgumentNullException(nameof(directives));
		}

		return new HeaderValue("cache-control", directives);
	}

	/// <summary>Creates the "content-disposition" header value.</summary>
	/// <param name="fileName">The name of the file.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="fileName"/> is <see langword="null"/>.</exception>
	public static HeaderValue ContentDisposition(string fileName)
	{
		if (fileName is null)
		{
			throw new ArgumentNullException(nameof(fileName));
		}

		return new HeaderValue("content-disposition", $"attachment;filename=\"{fileName}\"");
	}

	/// <summary>Creates the "content-type" header value.</summary>
	/// <param name="contentType">The media type of the content.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="contentType"/> is <see langword="null"/>.</exception>
	public static HeaderValue ContentType(string contentType)
	{
		if (contentType is null)
		{
			throw new ArgumentNullException(nameof(contentType));
		}

		return new HeaderValue("content-type", contentType);
	}

	/// <summary>Creates the "content-type" header value with a media type suitable for HTML.</summary>
	/// <returns>The response header value.</returns>
	public static HeaderValue ContentTypeHtml()
	{
		return new HeaderValue("content-type", "text/html;charset=utf-8");
	}

	/// <summary>Creates the "content-type" header value with a media type suitable for JSON.</summary>
	/// <returns>The response header value.</returns>
	public static HeaderValue ContentTypeJson()
	{
		return new HeaderValue("content-type", "application/json");
	}

	/// <summary>Creates a "set-cookie" header value which clears the cookie value and sets its expiry to now.</summary>
	/// <param name="name">The name of the cookie.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="name"/> must not be empty.</exception>
	public static HeaderValue DeleteCookie(string name)
	{
		try
		{
			return HeaderValue.CreateCookieValue(
				true,
				name,
				string.Empty,
				TimeSpan.Zero,
				"/",
				string.Empty,
				true,
				true);
		}
		catch (ArgumentException exception)
		{
			throw exception;
		}
	}

	/// <summary>Reads the "if-modified-since" header and converts it to a <see cref="DateTimeOffset"/>.</summary>
	/// <param name="request">The HTTP request.</param>
	/// <returns>A <see cref="DateTimeOffset"/> representing the last modification time of a resource.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="request"/> is <see langword="null"/>.</exception>
	public static DateTimeOffset IfModifiedSince(Request request)
	{
		if (request is null)
		{
			throw new ArgumentNullException(nameof(request));
		}

		return DateTimeOffset.TryParseExact(
			request.Header("if-modified-since"),
			HeaderValue.LastModifiedFormat,
			CultureInfo.InvariantCulture,
			DateTimeStyles.AssumeUniversal,
			out var result)
			? result
			: DateTime.MinValue;
	}

	/// <summary>Creates the "last-modified" header value.</summary>
	/// <param name="lastModified">The date and time the resource was last modified.</param>
	/// <returns>The response header value.</returns>
	public static HeaderValue LastModified(DateTimeOffset lastModified)
	{
		return new HeaderValue(
			"last-modified",
			lastModified.UtcDateTime.ToString(HeaderValue.LastModifiedFormat, CultureInfo.InvariantCulture));
	}

	/// <summary>Creates the "location" header value.</summary>
	/// <param name="location">The location of the resource.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="location"/> is <see langword="null"/>.</exception>
	public static HeaderValue Location(string location)
	{
		if (location is null)
		{
			throw new ArgumentNullException(nameof(location));
		}

		return new HeaderValue("location", location);
	}

	/// <summary>Creates a persistent cookie which will expire after the amount of time passed to <paramref name="maxAge"/>.</summary>
	/// <param name="name">The name of the cookie.</param>
	/// <param name="value">The value of the cookie.</param>
	/// <param name="maxAge">The length of time before the cookie expires.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="name"/> or <paramref name="value"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="name"/> and <paramref name="value"/> must not be empty.</exception>
	public static HeaderValue PersistentCookie(string name, string value, TimeSpan maxAge)
	{
		try
		{
			return HeaderValue.CreateCookieValue(true, name, value, maxAge, "/", string.Empty, true, true);
		}
		catch (ArgumentException exception)
		{
			throw exception;
		}
	}

	/// <summary>Creates a persistent cookie which will expire after the amount of time passed to <paramref name="maxAge"/>.</summary>
	/// <param name="name">The name of the cookie.</param>
	/// <param name="value">The value of the cookie.</param>
	/// <param name="maxAge">The length of time before the cookie expires.</param>
	/// <param name="path">The path to which the cookie is scoped.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="name"/>, <paramref name="value"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="name"/>, <paramref name="value"/> and <paramref name="path"/> must not be empty. <paramref name="path"/> must start with a forward slash.</exception>
	public static HeaderValue PersistentCookie(string name, string value, TimeSpan maxAge, string path)
	{
		try
		{
			return HeaderValue.CreateCookieValue(true, name, value, maxAge, path, string.Empty, true, true);
		}
		catch (Exception exception)
		{
			throw exception;
		}
	}

	/// <summary>Creates a persistent cookie which will expire after the amount of time passed to <paramref name="maxAge"/>.</summary>
	/// <param name="name">The name of the cookie.</param>
	/// <param name="value">The value of the cookie.</param>
	/// <param name="maxAge">The length of time before the cookie expires.</param>
	/// <param name="path">The path to which the cookie is scoped.</param>
	/// <param name="domain">The domain to which the cookie is scoped.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="name"/>, <paramref name="value"/>, <paramref name="path"/> or <paramref name="domain"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="name"/>, <paramref name="value"/> and <paramref name="path"/> must not be empty. <paramref name="path"/> must start with a forward slash.</exception>
	public static HeaderValue PersistentCookie(string name, string value, TimeSpan maxAge, string path, string domain)
	{
		try
		{
			return HeaderValue.CreateCookieValue(true, name, value, maxAge, path, domain, true, true);
		}
		catch (ArgumentException exception)
		{
			throw exception;
		}
	}

	/// <summary>Creates a persistent cookie which will expire after the amount of time passed to <paramref name="maxAge"/>.</summary>
	/// <param name="name">The name of the cookie.</param>
	/// <param name="value">The value of the cookie.</param>
	/// <param name="maxAge">The length of time before the cookie expires.</param>
	/// <param name="path">The path to which the cookie is scoped.</param>
	/// <param name="domain">The domain to which the cookie is scoped.</param>
	/// <param name="httpOnly">A value indicating whether the cookie is sent only via HTTP and HTTPS, i.e. it is not available to client-side scripts. Defaults to <see langword="true"/>.</param>
	/// <param name="secure">A value indicating whether the cookie will only be transmitted over secure connections (typically HTTPS). Defaults to <see langword="true"/>.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="name"/>, <paramref name="value"/>, <paramref name="path"/> or <paramref name="domain"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="name"/>, <paramref name="value"/> and <paramref name="path"/> must not be empty. <paramref name="path"/> must start with a forward slash.</exception>
	public static HeaderValue PersistentCookie(
		string name,
		string value,
		TimeSpan maxAge,
		string path,
		string domain,
		bool httpOnly,
		bool secure)
	{
		try
		{
			return HeaderValue.CreateCookieValue(true, name, value, maxAge, path, domain, httpOnly, secure);
		}
		catch (ArgumentException exception)
		{
			throw exception;
		}
	}

	/// <summary>Creates a session cookie which will expire when the session ends.</summary>
	/// <param name="name">The name of the cookie.</param>
	/// <param name="value">The value of the cookie.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="name"/> or <paramref name="value"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="name"/> and <paramref name="value"/> must not be empty.</exception>
	public static HeaderValue SessionCookie(string name, string value)
	{
		try
		{
			return HeaderValue.CreateCookieValue(false, name, value, TimeSpan.Zero, "/", string.Empty, true, true);
		}
		catch (ArgumentException exception)
		{
			throw exception;
		}
	}

	/// <summary>Creates a session cookie which will expire when the session ends.</summary>
	/// <param name="name">The name of the cookie.</param>
	/// <param name="value">The value of the cookie.</param>
	/// <param name="path">The path to which the cookie is scoped.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="name"/>, <paramref name="value"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="name"/>, <paramref name="value"/> and <paramref name="path"/> must not be empty. <paramref name="path"/> must start with a forward slash.</exception>
	public static HeaderValue SessionCookie(string name, string value, string path)
	{
		try
		{
			return HeaderValue.CreateCookieValue(false, name, value, TimeSpan.Zero, path, string.Empty, true, true);
		}
		catch (ArgumentException exception)
		{
			throw exception;
		}
	}

	/// <summary>Creates a session cookie which will expire when the session ends.</summary>
	/// <param name="name">The name of the cookie.</param>
	/// <param name="value">The value of the cookie.</param>
	/// <param name="path">The path to which the cookie is scoped.</param>
	/// <param name="domain">The domain to which the cookie is scoped.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="name"/>, <paramref name="value"/>, <paramref name="path"/> or <paramref name="domain"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="name"/>, <paramref name="value"/> and <paramref name="path"/> must not be empty. <paramref name="path"/> must start with a forward slash.</exception>
	public static HeaderValue SessionCookie(string name, string value, string path, string domain)
	{
		try
		{
			return HeaderValue.CreateCookieValue(false, name, value, TimeSpan.Zero, path, domain, true, true);
		}
		catch (ArgumentException exception)
		{
			throw exception;
		}
	}

	/// <summary>Creates a session cookie which will expire when the session ends.</summary>
	/// <param name="name">The name of the cookie.</param>
	/// <param name="value">The value of the cookie.</param>
	/// <param name="path">The path to which the cookie is scoped.</param>
	/// <param name="domain">The domain to which the cookie is scoped.</param>
	/// <param name="httpOnly">A value indicating whether the cookie is sent only via HTTP and HTTPS, i.e. it is not available to client-side scripts. Defaults to <see langword="true"/>.</param>
	/// <param name="secure">A value indicating whether the cookie will only be transmitted over secure connections (typically HTTPS). Defaults to <see langword="true"/>.</param>
	/// <returns>The response header value.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="name"/>, <paramref name="value"/>, <paramref name="path"/> or <paramref name="domain"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="name"/>, <paramref name="value"/> and <paramref name="path"/> must not be empty. <paramref name="path"/> must start with a forward slash.</exception>
	public static HeaderValue SessionCookie(
		string name,
		string value,
		string path,
		string domain,
		bool httpOnly,
		bool secure)
	{
		try
		{
			return HeaderValue.CreateCookieValue(false, name, value, TimeSpan.Zero, path, domain, httpOnly, secure);
		}
		catch (ArgumentException exception)
		{
			throw exception;
		}
	}

	/// <inheritdoc/>
	public bool Equals(HeaderValue other)
	{
		return this.Name == other.Name && this.Value == other.Value;
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj)
	{
		return obj is HeaderValue other && this.Equals(other);
	}

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return HashCode.Combine(this.Name, this.Value);
	}

	private static HeaderValue CreateCookieValue(
		bool persistent,
		string name,
		string value,
		TimeSpan maxAge,
		string path,
		string domain,
		bool httpOnly,
		bool secure)
	{
		if (name is null)
		{
			throw new ArgumentNullException(nameof(name));
		}

		if (name.AsSpan().Trim().IsEmpty)
		{
			throw new ArgumentException("The cookie name must not be empty.", nameof(name));
		}

		if (value is null)
		{
			throw new ArgumentNullException(nameof(value));
		}

		if (path is null)
		{
			throw new ArgumentNullException(nameof(path));
		}

		if (path.AsSpan().Trim().IsEmpty || path[0] != '/')
		{
			throw new ArgumentException("The cookie path must begin with a \"/\".", nameof(path));
		}

		if (domain is null)
		{
			throw new ArgumentNullException(nameof(domain));
		}

		var stringBuilder = new StringBuilder(256);

		stringBuilder.Append(name);
		stringBuilder.Append('=');
		stringBuilder.Append(WebUtility.UrlEncode(value));
		stringBuilder.Append(";path=");
		stringBuilder.Append(path);

		if (persistent)
		{
			stringBuilder.Append(";max-age=");
			stringBuilder.Append((int)maxAge.TotalSeconds);
		}

		if (domain.Length > 0)
		{
			stringBuilder.Append(";domain=");
			stringBuilder.Append(domain);
		}

		if (secure)
		{
			stringBuilder.Append(";secure");
		}

		if (httpOnly)
		{
			stringBuilder.Append(";httponly");
		}

		return new HeaderValue("set-cookie", stringBuilder.ToString());
	}
}
