using System;
using System.Collections.Generic;

namespace Mundane;

/// <inheritdoc/>
public sealed class Dependencies : DependencyFinder
{
	private readonly Dictionary<Type, Dependency> dependencyLookup;

	/// <summary>Initializes a new instance of the <see cref="Dependencies"/> class.</summary>
	public Dependencies()
	{
		this.dependencyLookup = new Dictionary<Type, Dependency>(0);
	}

	/// <summary>Initializes a new instance of the <see cref="Dependencies"/> class.</summary>
	/// <param name="dependencies">The dependencies to register.</param>
	/// <exception cref="ArgumentNullException"><paramref name="dependencies"/> or any of its elements is <see langword="null"/>.</exception>
	/// <exception cref="DuplicateDependencyRegistered">A dependency type appears in <paramref name="dependencies"/> more than once.</exception>
	public Dependencies(params Dependency[] dependencies)
	{
		ArgumentNullException.ThrowIfNull(dependencies);

		this.dependencyLookup = new Dictionary<Type, Dependency>(dependencies.Length);

		foreach (var dependency in dependencies)
		{
			ArgumentNullException.ThrowIfNull(dependency, nameof(dependencies));

			if (this.dependencyLookup.ContainsKey(dependency.DependencyType))
			{
				throw new DuplicateDependencyRegistered(dependency.DependencyType);
			}

			this.dependencyLookup.Add(dependency.DependencyType, dependency);
		}
	}

	/// <inheritdoc/>
	public T Find<T>(Request request)
		where T : notnull
	{
		ArgumentNullException.ThrowIfNull(request);

		if (!this.dependencyLookup.TryGetValue(typeof(T), out var dependency))
		{
			throw new DependencyNotFound(typeof(T));
		}

		return ((Dependency<T>)dependency).CreateDependency(request);
	}
}
