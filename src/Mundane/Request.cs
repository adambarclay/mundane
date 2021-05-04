using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Mundane
{
	/// <summary>The HTTP request.</summary>
	public interface Request
	{
		/// <summary>Gets the collection of cookies.</summary>
		EnumerableCollection<KeyValuePair<string, string>> AllCookies { get; }

		/// <summary>Gets the collection of uploaded files.</summary>
		EnumerableCollection<KeyValuePair<string, FileUpload>> AllFileParameters { get; }

		/// <summary>Gets the collection of form parameters.</summary>
		EnumerableCollection<KeyValuePair<string, string>> AllFormParameters { get; }

		/// <summary>Gets the collection of HTTP request headers.</summary>
		EnumerableCollection<KeyValuePair<string, string>> AllHeaders { get; }

		/// <summary>Gets the collection of query parameters.</summary>
		EnumerableCollection<KeyValuePair<string, string>> AllQueryParameters { get; }

		/// <summary>Gets the body of the request.</summary>
		Stream Body { get; }

		/// <summary>Gets the name of the application host. e.g. "www.example.com".</summary>
		public string Host { get; }

		/// <summary>Gets the HTTP request method.</summary>
		string Method { get; }

		/// <summary>Gets the path of the request.</summary>
		string Path { get; }

		/// <summary>Gets the base path the application is hosted under, e.g. if your application is hosted under "www.example.com/my-app", <see cref="PathBase"/> will be "my-app".</summary>
		public string PathBase { get; }

		/// <summary>Gets a cancellation token which is signalled when the user cancels the request.</summary>
		CancellationToken RequestAborted { get; }

		/// <summary>Gets the request scheme. e.g. "https".</summary>
		public string Scheme { get; }

		/// <summary>Gets the cookie value with the specified name.</summary>
		/// <param name="cookieName">The name of the cookie.</param>
		/// <returns>The cookie value.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="cookieName"/> is <see langword="null"/>.</exception>
		string Cookie(string cookieName);

		/// <summary>Gets a value indicating whether the cookies collection contains the specified cookie.</summary>
		/// <param name="cookieName">The name of the cookie.</param>
		/// <returns>true if the collection contains the cookie, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="cookieName"/> is <see langword="null"/>.</exception>
		bool CookieExists(string cookieName);

		/// <summary>Returns an instance of the dependency registered for the requested type <typeparamref name="T"/>.</summary>
		/// <typeparam name="T">The type of the dependency.</typeparam>
		/// <returns>An instance of the dependency registered for the requested type <typeparamref name="T"/>.</returns>
		/// <exception cref="DependencyNotFound">No concrete type has been registered for the requested dependency type.</exception>
		public T Dependency<T>()
			where T : notnull;

		/// <summary>Gets the uploaded file with the specified parameter name.</summary>
		/// <param name="parameterName">The name of the file parameter.</param>
		/// <returns>The uploaded file.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		FileUpload File(string parameterName);

		/// <summary>Gets a value indicating whether the uploaded files collection contains the specified file.</summary>
		/// <param name="parameterName">The name of the file parameter.</param>
		/// <returns>true if the collection contains the file, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		bool FileExists(string parameterName);

		/// <summary>Gets the form parameter with the specified name.</summary>
		/// <param name="parameterName">The name of the form parameter.</param>
		/// <returns>The value of the form parameter.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		string Form(string parameterName);

		/// <summary>Gets a value indicating whether the form collection contains the specified form parameter.</summary>
		/// <param name="parameterName">The name of the form parameter.</param>
		/// <returns>true if the collection contains the form parameter, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		bool FormExists(string parameterName);

		/// <summary>Gets the header value with the specified name.</summary>
		/// <param name="headerName">The name of the header.</param>
		/// <returns>The value of the query parameter.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="headerName"/> is <see langword="null"/>.</exception>
		string Header(string headerName);

		/// <summary>Gets a value indicating whether the header collection contains the specified header.</summary>
		/// <param name="headerName">The name of the header.</param>
		/// <returns>true if the collection contains the header, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="headerName"/> is <see langword="null"/>.</exception>
		public bool HeaderExists(string headerName);

		/// <summary>Gets the query parameter with the specified name.</summary>
		/// <param name="parameterName">The name of the query parameter.</param>
		/// <returns>The value of the query parameter.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		string Query(string parameterName);

		/// <summary>Gets a value indicating whether the query collection contains the specified query parameter.</summary>
		/// <param name="parameterName">The name of the query parameter.</param>
		/// <returns>true if the collection contains the query parameter, otherwise false.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		bool QueryExists(string parameterName);

		/// <summary>Gets the route parameter with the specified name.</summary>
		/// <param name="parameterName">The name of the route parameter.</param>
		/// <returns>The value of the route parameter.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null"/>.</exception>
		/// <exception cref="RouteParameterNotFound"><paramref name="parameterName"/> is not one of the route parameters configured for this route.</exception>
		string Route(string parameterName);
	}
}
