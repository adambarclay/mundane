using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;

namespace Mundane
{
	/// <summary>The HTTP request.</summary>
	public sealed class Request
	{
		private readonly Dictionary<string, string> cookies;
		private readonly DependencyFinder dependencyFinder;
		private readonly Dictionary<string, string> form;
		private readonly Dictionary<string, string> headers;
		private readonly Dictionary<string, string> query;
		private readonly Dictionary<string, string> route;
		private readonly Dictionary<string, FileUpload> uploadedFiles;

		/// <summary>Initializes a new instance of the <see cref="Request"/> class.</summary>
		/// <param name="method">The HTTP method of the request.</param>
		/// <param name="path">The path of the request.</param>
		/// <param name="route">The parameters extracted from the route.</param>
		/// <param name="headers">The HTTP headers of the request.</param>
		/// <param name="body">The body of the request.</param>
		/// <param name="query">The collection of query parameters.</param>
		/// <param name="form">The collection of form parameters.</param>
		/// <param name="cookies">The collection of cookies.</param>
		/// <param name="uploadedFiles">The collection of uploaded files.</param>
		/// <param name="dependencyFinder">The dependency finder.</param>
		/// <param name="host">The host of the request.</param>
		/// <param name="requestAborted">A cancellation token which is signalled if the user cancels the request.</param>
		/// <exception cref="ArgumentNullException"><paramref name="method"/>, <paramref name="path"/>, <paramref name="route"/>, <paramref name="headers"/>, <paramref name="body"/>, <paramref name="query"/>, <paramref name="form"/>, <paramref name="cookies"/>, <paramref name="uploadedFiles"/> or <paramref name="host"/> is <see langword="null"/>.</exception>
		public Request(
			[DisallowNull] string method,
			[DisallowNull] string path,
			EnumerableDictionary<string, string> route,
			EnumerableDictionary<string, string> headers,
			[DisallowNull] Stream body,
			EnumerableDictionary<string, string> query,
			EnumerableDictionary<string, string> form,
			EnumerableDictionary<string, string> cookies,
			EnumerableDictionary<string, FileUpload> uploadedFiles,
			[DisallowNull] DependencyFinder dependencyFinder,
			[DisallowNull] RequestHost host,
			CancellationToken requestAborted)
		{
			if (method == null)
			{
				throw new ArgumentNullException(nameof(method));
			}

			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}

			if (route.Dictionary == null)
			{
				throw new ArgumentNullException(nameof(route));
			}

			if (headers.Dictionary == null)
			{
				throw new ArgumentNullException(nameof(headers));
			}

			if (body == null)
			{
				throw new ArgumentNullException(nameof(body));
			}

			if (query.Dictionary == null)
			{
				throw new ArgumentNullException(nameof(query));
			}

			if (form.Dictionary == null)
			{
				throw new ArgumentNullException(nameof(form));
			}

			if (cookies.Dictionary == null)
			{
				throw new ArgumentNullException(nameof(cookies));
			}

			if (uploadedFiles.Dictionary == null)
			{
				throw new ArgumentNullException(nameof(uploadedFiles));
			}

			if (dependencyFinder == null)
			{
				throw new ArgumentNullException(nameof(dependencyFinder));
			}

			if (host == null)
			{
				throw new ArgumentNullException(nameof(host));
			}

			this.Method = method;
			this.Path = path;
			this.route = route.Dictionary;
			this.headers = headers.Dictionary;
			this.Body = body;
			this.query = query.Dictionary;
			this.form = form.Dictionary;
			this.cookies = cookies.Dictionary;
			this.uploadedFiles = uploadedFiles.Dictionary;
			this.dependencyFinder = dependencyFinder;
			this.Host = host;
			this.RequestAborted = requestAborted;
		}

		/// <summary>Gets the collection of cookies.</summary>
		public EnumerableDictionary<string, string> AllCookies
		{
			get
			{
				return new EnumerableDictionary<string, string>(this.cookies);
			}
		}

		/// <summary>Gets the collection of uploaded files.</summary>
		public EnumerableDictionary<string, FileUpload> AllFileParameters
		{
			get
			{
				return new EnumerableDictionary<string, FileUpload>(this.uploadedFiles);
			}
		}

		/// <summary>Gets the collection of form parameters.</summary>
		public EnumerableDictionary<string, string> AllFormParameters
		{
			get
			{
				return new EnumerableDictionary<string, string>(this.form);
			}
		}

		/// <summary>Gets the collection of HTTP request headers.</summary>
		public EnumerableDictionary<string, string> AllHeaders
		{
			get
			{
				return new EnumerableDictionary<string, string>(this.headers);
			}
		}

		/// <summary>Gets the collection of query parameters.</summary>
		public EnumerableDictionary<string, string> AllQueryParameters
		{
			get
			{
				return new EnumerableDictionary<string, string>(this.query);
			}
		}

		/// <summary>Gets the body of the request.</summary>
		[NotNull]
		public Stream Body { get; }

		/// <summary>Gets the host of the request.</summary>
		[NotNull]
		public RequestHost Host { get; }

		/// <summary>Gets the HTTP request method.</summary>
		[NotNull]
		public string Method { get; }

		/// <summary>Gets the path of the request.</summary>
		[NotNull]
		public string Path { get; }

		/// <summary>Gets a cancellation token which is signalled when the user cancels the request.</summary>
		public CancellationToken RequestAborted { get; }

		/// <summary>Gets the cookie value with the specified name.</summary>
		/// <param name="cookieName">The name of the cookie.</param>
		/// <returns>The cookie value.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="cookieName"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public string Cookie([DisallowNull] string cookieName)
		{
			if (cookieName == null)
			{
				throw new ArgumentNullException(nameof(cookieName));
			}

			return this.cookies.TryGetValue(cookieName, out var value) ? value : string.Empty;
		}

		/// <summary>Gets a value indicating whether the cookies collection contains the specified cookie.</summary>
		/// <param name="cookieName">The name of the cookie.</param>
		/// <returns>true if the collection contains the cookie, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="cookieName"/> is <see langword="null"/>.</exception>
		public bool CookieExists([DisallowNull] string cookieName)
		{
			if (cookieName == null)
			{
				throw new ArgumentNullException(nameof(cookieName));
			}

			return this.cookies.ContainsKey(cookieName);
		}

		/// <summary>Returns an instance of the dependency registered for the requested type <typeparamref name="T"/>.</summary>
		/// <typeparam name="T">The type of the dependency.</typeparam>
		/// <returns>An instance of the dependency registered for the requested type <typeparamref name="T"/>.</returns>
		/// <exception cref="DependencyNotFound">No concrete type has been registered for the requested dependency type.</exception>
		[return: NotNull]
		public T Dependency<T>()
			where T : notnull
		{
			var dependency = this.dependencyFinder.Find<T>(this);

			if (dependency == null)
			{
				throw new DependencyNotFound(typeof(T));
			}

			return dependency;
		}

		/// <summary>Gets the uploaded file with the specified parameter name.</summary>
		/// <param name="parameterName">The name of the file parameter.</param>
		/// <returns>The uploaded file.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public FileUpload File([DisallowNull] string parameterName)
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException(nameof(parameterName));
			}

			return this.uploadedFiles.TryGetValue(parameterName, out var value) ? value : FileUpload.Unknown;
		}

		/// <summary>Gets a value indicating whether the uploaded files collection contains the specified file.</summary>
		/// <param name="parameterName">The name of the file parameter.</param>
		/// <returns>true if the collection contains the file, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		public bool FileExists([DisallowNull] string parameterName)
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException(nameof(parameterName));
			}

			return this.uploadedFiles.ContainsKey(parameterName);
		}

		/// <summary>Gets the form parameter with the specified name.</summary>
		/// <param name="parameterName">The name of the form parameter.</param>
		/// <returns>The value of the form parameter.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public string Form([DisallowNull] string parameterName)
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException(nameof(parameterName));
			}

			return this.form.TryGetValue(parameterName, out var value) ? value : string.Empty;
		}

		/// <summary>Gets a value indicating whether the form collection contains the specified form parameter.</summary>
		/// <param name="parameterName">The name of the form parameter.</param>
		/// <returns>true if the collection contains the form parameter, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		public bool FormExists([DisallowNull] string parameterName)
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException(nameof(parameterName));
			}

			return this.form.ContainsKey(parameterName);
		}

		/// <summary>Gets the header value with the specified name.</summary>
		/// <param name="headerName">The name of the header.</param>
		/// <returns>The value of the query parameter.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="headerName"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public string Header([DisallowNull] string headerName)
		{
			if (headerName == null)
			{
				throw new ArgumentNullException(nameof(headerName));
			}

			return this.headers.TryGetValue(headerName, out var value) ? value : string.Empty;
		}

		/// <summary>Gets a value indicating whether the header collection contains the specified header.</summary>
		/// <param name="headerName">The name of the header.</param>
		/// <returns>true if the collection contains the header, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="headerName"/> is <see langword="null"/>.</exception>
		public bool HeaderExists([DisallowNull] string headerName)
		{
			if (headerName == null)
			{
				throw new ArgumentNullException(nameof(headerName));
			}

			return this.headers.ContainsKey(headerName);
		}

		/// <summary>Gets the query parameter with the specified name.</summary>
		/// <param name="parameterName">The name of the query parameter.</param>
		/// <returns>The value of the query parameter.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public string Query([DisallowNull] string parameterName)
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException(nameof(parameterName));
			}

			return this.query.TryGetValue(parameterName, out var value) ? value : string.Empty;
		}

		/// <summary>Gets a value indicating whether the query collection contains the specified query parameter.</summary>
		/// <param name="parameterName">The name of the query parameter.</param>
		/// <returns>true if the collection contains the query parameter, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		public bool QueryExists([DisallowNull] string parameterName)
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException(nameof(parameterName));
			}

			return this.query.ContainsKey(parameterName);
		}

		/// <summary>Gets the route parameter with the specified name.</summary>
		/// <param name="parameterName">The name of the route parameter.</param>
		/// <returns>The value of the route parameter.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public string Route([DisallowNull] string parameterName)
		{
			if (parameterName == null)
			{
				throw new ArgumentNullException(nameof(parameterName));
			}

			return this.route.TryGetValue(parameterName, out var value) ? value : string.Empty;
		}
	}
}
