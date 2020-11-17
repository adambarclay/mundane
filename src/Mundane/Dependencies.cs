using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Mundane
{
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
		public Dependencies([DisallowNull] params Dependency[] dependencies)
		{
			if (dependencies == null)
			{
				throw new ArgumentNullException(nameof(dependencies));
			}

			this.dependencyLookup = new Dictionary<Type, Dependency>(dependencies.Length);

			foreach (var dependency in dependencies)
			{
				if (dependency == null)
				{
					throw new ArgumentNullException(nameof(dependencies));
				}

				if (this.dependencyLookup.ContainsKey(dependency.DependencyType))
				{
					throw new DuplicateDependencyRegistered(dependency.DependencyType);
				}

				this.dependencyLookup.Add(dependency.DependencyType, dependency);
			}
		}

		/// <inheritdoc/>
		[return: NotNull]
		public T Find<T>([DisallowNull] Request request)
			where T : notnull
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			if (!this.dependencyLookup.TryGetValue(typeof(T), out var dependency))
			{
				throw new DependencyNotFound(typeof(T));
			}

			return ((Dependency<T>)dependency).CreateDependencyAction(request);
		}
	}
}
