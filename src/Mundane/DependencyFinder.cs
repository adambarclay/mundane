using System;

namespace Mundane;

/// <summary>Finds dependencies requested by the application.</summary>
public interface DependencyFinder
{
	/// <summary>Returns an instance of the dependency registered for the requested type <typeparamref name="T"/>.</summary>
	/// <typeparam name="T">The type of the dependency.</typeparam>
	/// <param name="request">The current request.</param>
	/// <returns>An instance of the dependency registered for the requested type <typeparamref name="T"/>.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="request"/> is <see langword="null"/>.</exception>
	/// <exception cref="DependencyNotFound">No concrete type has been registered for the requested dependency type.</exception>
	T Find<T>(Request request)
		where T : notnull;
}
