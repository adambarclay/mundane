using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Mundane
{
	/// <summary>The HTTP response.</summary>
	public sealed class Response
	{
		private static readonly Func<ResponseStream, Task> EmptyBody = stream => Task.CompletedTask;

		private readonly Func<ResponseStream, Task> bodyWriter;
		private readonly List<HeaderValue> headers;
		private readonly int statusCode;

		/// <summary>Initializes a new instance of the <see cref="Response"/> class.</summary>
		/// <param name="statusCode">The HTTP status code.</param>
		public Response(int statusCode)
			: this(statusCode, Response.EmptyBody)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="Response"/> class.</summary>
		/// <param name="statusCode">The HTTP response status code.</param>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		public Response(int statusCode, [DisallowNull] Func<ResponseStream, Task> bodyWriter)
		{
			if (bodyWriter == null)
			{
				throw new ArgumentNullException(nameof(bodyWriter));
			}

			this.statusCode = statusCode;
			this.bodyWriter = bodyWriter;
			this.headers = new List<HeaderValue>();
		}

		/// <summary>Creates a "bad request" response (status code 400).</summary>
		/// <returns>The HTTP response.</returns>
		[return: NotNull]
		public static Response BadRequest()
		{
			return new Response(400);
		}

		/// <summary>Creates a "bad request" response (status code 400).</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response BadRequest([DisallowNull] Func<ResponseStream, Task> bodyWriter)
		{
			return new Response(400, bodyWriter).AddHeader(HeaderValue.ContentTypeHtml());
		}

		/// <summary>Creates a file download response.</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <param name="contentType">The media type of the file.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> or <paramref name="contentType"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response File(
			[DisallowNull] Func<ResponseStream, Task> bodyWriter,
			[DisallowNull] string contentType)
		{
			return new Response(200, bodyWriter).AddHeader(HeaderValue.ContentType(contentType));
		}

		/// <summary>Creates a file download response with the specified file name.</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <param name="contentType">The media type of the file.</param>
		/// <param name="fileName">The name the file will have when it is downloaded.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/>, <paramref name="contentType"/> or <paramref name="fileName"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response File(
			[DisallowNull] Func<ResponseStream, Task> bodyWriter,
			[DisallowNull] string contentType,
			[DisallowNull] string fileName)
		{
			return new Response(200, bodyWriter).AddHeader(HeaderValue.ContentType(contentType))
				.AddHeader(HeaderValue.ContentDisposition(fileName));
		}

		/// <summary>Creates a "forbidden" response (status code 403).</summary>
		/// <returns>The HTTP response.</returns>
		[return: NotNull]
		public static Response Forbidden()
		{
			return new Response(403);
		}

		/// <summary>Creates a "forbidden" response (status code 403).</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response Forbidden([DisallowNull] Func<ResponseStream, Task> bodyWriter)
		{
			return new Response(403, bodyWriter).AddHeader(HeaderValue.ContentTypeHtml());
		}

		/// <summary>Creates an "internal server error" response (status code 500).</summary>
		/// <returns>The HTTP response.</returns>
		[return: NotNull]
		public static Response InternalServerError()
		{
			return new Response(500);
		}

		/// <summary>Creates an "internal server error" response (status code 500).</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response InternalServerError([DisallowNull] Func<ResponseStream, Task> bodyWriter)
		{
			return new Response(500, bodyWriter).AddHeader(HeaderValue.ContentTypeHtml());
		}

		/// <summary>Creates an "ok" response (status code 200), with a media type of "application/json".</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response Json([DisallowNull] Func<ResponseStream, Task> bodyWriter)
		{
			return new Response(200, bodyWriter).AddHeader(HeaderValue.ContentTypeJson());
		}

		/// <summary>Creates a response with the specified status code, and a media type of "application/json".</summary>
		/// <param name="statusCode">The HTTP response status code.</param>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response Json(int statusCode, [DisallowNull] Func<ResponseStream, Task> bodyWriter)
		{
			return new Response(statusCode, bodyWriter).AddHeader(HeaderValue.ContentTypeJson());
		}

		/// <summary>Creates a "method not allowed" response (status code 405).</summary>
		/// <param name="allowedMethods">The allowed HTTP methods which will be sent in the "allow" header.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="allowedMethods"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response MethodNotAllowed([DisallowNull] IEnumerable<string> allowedMethods)
		{
			return new Response(405).AddHeader(HeaderValue.Allow(allowedMethods));
		}

		/// <summary>Creates a "not found" response (status code 404).</summary>
		/// <returns>The HTTP response.</returns>
		[return: NotNull]
		public static Response NotFound()
		{
			return new Response(404);
		}

		/// <summary>Creates a "not found" response (status code 404).</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response NotFound([DisallowNull] Func<ResponseStream, Task> bodyWriter)
		{
			return new Response(404, bodyWriter).AddHeader(HeaderValue.ContentTypeHtml());
		}

		/// <summary>Creates an "ok" response (status code 200).</summary>
		/// <returns>The HTTP response.</returns>
		[return: NotNull]
		public static Response Ok()
		{
			return new Response(200);
		}

		/// <summary>Creates an "ok" response (status code 200).</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response Ok([DisallowNull] Func<ResponseStream, Task> bodyWriter)
		{
			return new Response(200, bodyWriter).AddHeader(HeaderValue.ContentTypeHtml());
		}

		/// <summary>Creates a "redirect permanently" response (status code 301).</summary>
		/// <param name="location">The location to which the client is redirected.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="location"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response RedirectPermanently([DisallowNull] string location)
		{
			return new Response(301).AddHeader(HeaderValue.Location(location));
		}

		/// <summary>Creates a "redirect see other" response (status code 303).</summary>
		/// <param name="location">The location to which the client is redirected.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="location"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response RedirectSeeOther([DisallowNull] string location)
		{
			return new Response(303).AddHeader(HeaderValue.Location(location));
		}

		/// <summary>Creates an "unauthorized" response (status code 401).</summary>
		/// <returns>The HTTP response.</returns>
		[return: NotNull]
		public static Response Unauthorized()
		{
			return new Response(401);
		}

		/// <summary>Creates an "unauthorized" response (status code 401).</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public static Response Unauthorized([DisallowNull] Func<ResponseStream, Task> bodyWriter)
		{
			return new Response(401, bodyWriter).AddHeader(HeaderValue.ContentTypeHtml());
		}

		/// <summary>Sets a header in the response.</summary>
		/// <param name="headerValue">The value of the header to set.</param>
		/// <returns>The HTTP response.</returns>
		[return: NotNull]
		public Response AddHeader(HeaderValue headerValue)
		{
			if (headerValue == default)
			{
				return this;
			}

			Debug.Assert(!headerValue.Name.AsSpan().Trim().IsEmpty);

			this.headers.Add(headerValue);

			return this;
		}

		internal MundaneEngineResponse BuildResponse([DisallowNull] Request request)
		{
			return new MundaneEngineResponse(request, this.statusCode, this.headers, this.bodyWriter);
		}

		internal MundaneEngineResponse BuildResponseWithNoBody([DisallowNull] Request request)
		{
			return new MundaneEngineResponse(request, this.statusCode, this.headers, Response.EmptyBody);
		}
	}
}
