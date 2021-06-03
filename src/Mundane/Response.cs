using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Mundane
{
	/// <summary>Writes the response body to the output stream.</summary>
	/// <param name="responseStream">The response output stream.</param>
	public delegate ValueTask BodyWriter(ResponseStream responseStream);

	/// <summary>The HTTP response.</summary>
	public sealed class Response
	{
		private static readonly BodyWriter EmptyBody = _ => ValueTask.CompletedTask;

		private readonly BodyWriter bodyWriter;
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
		public Response(int statusCode, BodyWriter bodyWriter)
		{
			if (bodyWriter is null)
			{
				throw new ArgumentNullException(nameof(bodyWriter));
			}

			this.statusCode = statusCode;
			this.bodyWriter = bodyWriter;
			this.headers = new List<HeaderValue>();
		}

		/// <summary>Creates a "bad request" response (status code 400).</summary>
		/// <returns>The HTTP response.</returns>
		public static Response BadRequest()
		{
			return new Response(400);
		}

		/// <summary>Creates a "bad request" response (status code 400).</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		public static Response BadRequest(BodyWriter bodyWriter)
		{
			return new Response(400, bodyWriter).AddHeader(HeaderValue.ContentTypeHtml());
		}

		/// <summary>Creates a file download response.</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <param name="contentType">The media type of the file.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> or <paramref name="contentType"/> is <see langword="null"/>.</exception>
		public static Response File(BodyWriter bodyWriter, string contentType)
		{
			return new Response(200, bodyWriter).AddHeader(HeaderValue.ContentType(contentType));
		}

		/// <summary>Creates a file download response with the specified file name.</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <param name="contentType">The media type of the file.</param>
		/// <param name="fileName">The name the file will have when it is downloaded.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/>, <paramref name="contentType"/> or <paramref name="fileName"/> is <see langword="null"/>.</exception>
		public static Response File(BodyWriter bodyWriter, string contentType, string fileName)
		{
			return new Response(200, bodyWriter).AddHeader(HeaderValue.ContentType(contentType))
				.AddHeader(HeaderValue.ContentDisposition(fileName));
		}

		/// <summary>Creates a "forbidden" response (status code 403).</summary>
		/// <returns>The HTTP response.</returns>
		public static Response Forbidden()
		{
			return new Response(403);
		}

		/// <summary>Creates a "forbidden" response (status code 403).</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		public static Response Forbidden(BodyWriter bodyWriter)
		{
			return new Response(403, bodyWriter).AddHeader(HeaderValue.ContentTypeHtml());
		}

		/// <summary>Creates an "internal server error" response (status code 500).</summary>
		/// <returns>The HTTP response.</returns>
		public static Response InternalServerError()
		{
			return new Response(500);
		}

		/// <summary>Creates an "internal server error" response (status code 500).</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		public static Response InternalServerError(BodyWriter bodyWriter)
		{
			return new Response(500, bodyWriter).AddHeader(HeaderValue.ContentTypeHtml());
		}

		/// <summary>Creates an "ok" response (status code 200), with a media type of "application/json".</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		public static Response Json(BodyWriter bodyWriter)
		{
			return new Response(200, bodyWriter).AddHeader(HeaderValue.ContentTypeJson());
		}

		/// <summary>Creates a response with the specified status code, and a media type of "application/json".</summary>
		/// <param name="statusCode">The HTTP response status code.</param>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		public static Response Json(int statusCode, BodyWriter bodyWriter)
		{
			return new Response(statusCode, bodyWriter).AddHeader(HeaderValue.ContentTypeJson());
		}

		/// <summary>Creates a "method not allowed" response (status code 405).</summary>
		/// <param name="allowedMethods">The allowed HTTP methods which will be sent in the "allow" header.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="allowedMethods"/> is <see langword="null"/>.</exception>
		public static Response MethodNotAllowed(IEnumerable<string> allowedMethods)
		{
			return new Response(405).AddHeader(HeaderValue.Allow(allowedMethods));
		}

		/// <summary>Creates a "not found" response (status code 404).</summary>
		/// <returns>The HTTP response.</returns>
		public static Response NotFound()
		{
			return new Response(404);
		}

		/// <summary>Creates a "not found" response (status code 404).</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		public static Response NotFound(BodyWriter bodyWriter)
		{
			return new Response(404, bodyWriter).AddHeader(HeaderValue.ContentTypeHtml());
		}

		/// <summary>Creates an "ok" response (status code 200).</summary>
		/// <returns>The HTTP response.</returns>
		public static Response Ok()
		{
			return new Response(200);
		}

		/// <summary>Creates an "ok" response (status code 200).</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		public static Response Ok(BodyWriter bodyWriter)
		{
			return new Response(200, bodyWriter).AddHeader(HeaderValue.ContentTypeHtml());
		}

		/// <summary>Creates a "redirect permanently" response (status code 301).</summary>
		/// <param name="location">The location to which the client is redirected.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="location"/> is <see langword="null"/>.</exception>
		public static Response RedirectPermanently(string location)
		{
			return new Response(301).AddHeader(HeaderValue.Location(location));
		}

		/// <summary>Creates a "redirect see other" response (status code 303).</summary>
		/// <param name="location">The location to which the client is redirected.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="location"/> is <see langword="null"/>.</exception>
		public static Response RedirectSeeOther(string location)
		{
			return new Response(303).AddHeader(HeaderValue.Location(location));
		}

		/// <summary>Creates an "unauthorized" response (status code 401).</summary>
		/// <returns>The HTTP response.</returns>
		public static Response Unauthorized()
		{
			return new Response(401);
		}

		/// <summary>Creates an "unauthorized" response (status code 401).</summary>
		/// <param name="bodyWriter">Writes the response body to the output stream.</param>
		/// <returns>The HTTP response.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="bodyWriter"/> is <see langword="null"/>.</exception>
		public static Response Unauthorized(BodyWriter bodyWriter)
		{
			return new Response(401, bodyWriter).AddHeader(HeaderValue.ContentTypeHtml());
		}

		/// <summary>Sets a header in the response.</summary>
		/// <param name="headerValue">The value of the header to set.</param>
		/// <returns>The HTTP response.</returns>
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

		internal MundaneEngineResponse BuildResponse(Request request)
		{
			return new MundaneEngineResponse(request, this.statusCode, this.headers, this.bodyWriter);
		}

		internal MundaneEngineResponse BuildResponseWithNoBody(Request request)
		{
			return new MundaneEngineResponse(request, this.statusCode, this.headers, Response.EmptyBody);
		}
	}
}
