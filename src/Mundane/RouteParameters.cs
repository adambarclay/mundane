using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Mundane;

/// <summary>The route parameters.</summary>
public sealed class RouteParameters : IEnumerable<KeyValuePair<string, string>>
{
	private readonly Dictionary<string, string> routeParameters;

	/// <summary>Initializes a new instance of the <see cref="RouteParameters"/> class.</summary>
	/// <param name="routeParameters">The dictionary containing the route parameter data.</param>
	/// <exception cref="ArgumentNullException"><paramref name="routeParameters"/> is <see langword="null"/>.</exception>
	public RouteParameters(Dictionary<string, string> routeParameters)
	{
		if (routeParameters is null)
		{
			throw new ArgumentNullException(nameof(routeParameters));
		}

		this.routeParameters = routeParameters;
	}

	/// <inheritdoc/>
	public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
	{
		return this.routeParameters.GetEnumerator();
	}

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	/// <summary>Gets the value that is associated with the specified key.</summary>
	/// <param name="key">The key to locate.</param>
	/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter. This parameter is passed uninitialized.</param>
	/// <returns><see langword="true"/> if the <see cref="routeParameters"/> contains an element that has the specified key; otherwise, <see langword="false"/>.</returns>
	public bool TryGetValue(string key, [MaybeNullWhen(false)] out string value)
	{
		return this.routeParameters.TryGetValue(key, out value);
	}
}
