using System;
using System.Diagnostics.CodeAnalysis;

namespace Mundane
{
	/// <summary>The method for creating an object which fulfils the dependency using the current request.</summary>
	/// <typeparam name="T">The type of the dependency.</typeparam>
	/// <param name="request">The current request.</param>
	/// <returns>An instance of the dependency registered for the requested type <typeparamref name="T"/>.</returns>
	[return: NotNull]
	public delegate T CreateDependency<out T>([DisallowNull] Request request);

	/// <summary>The method for creating an object which fulfils the dependency receiving no parameters.</summary>
	/// <typeparam name="T">The type of the dependency.</typeparam>
	/// <returns>An instance of the dependency registered for the requested type <typeparamref name="T"/>.</returns>
	[return: NotNull]
	public delegate T CreateDependencyNoParameters<out T>();

	/// <summary>Contains a method for creating an object which fulfils a dependency.</summary>
	/// <typeparam name="T">The type of the dependency.</typeparam>
	public sealed class Dependency<T> : Dependency
		where T : notnull
	{
		/// <summary>Initializes a new instance of the <see cref="Dependency{T}"/> class.</summary>
		/// <param name="dependency">The object which fulfils the dependency.</param>
		/// <exception cref="ArgumentNullException"><paramref name="dependency"/> is <see langword="null"/>.</exception>
		public Dependency([DisallowNull] T dependency)
			: base(typeof(T))
		{
			if (dependency == null)
			{
				throw new ArgumentNullException(nameof(dependency));
			}

			this.CreateDependency = _ => dependency;
		}

		/// <summary>Initializes a new instance of the <see cref="Dependency{T}"/> class.</summary>
		/// <param name="createDependency">The method for creating an object which fulfils the dependency.</param>
		/// <exception cref="ArgumentNullException"><paramref name="createDependency"/> is <see langword="null"/>.</exception>
		public Dependency([DisallowNull] CreateDependencyNoParameters<T> createDependency)
			: base(typeof(T))
		{
			if (createDependency == null)
			{
				throw new ArgumentNullException(nameof(createDependency));
			}

			this.CreateDependency = _ => createDependency();
		}

		/// <summary>Initializes a new instance of the <see cref="Dependency{T}"/> class.</summary>
		/// <param name="createDependency">The method for creating an object which fulfils the dependency.</param>
		/// <exception cref="ArgumentNullException"><paramref name="createDependency"/> is <see langword="null"/>.</exception>
		public Dependency([DisallowNull] CreateDependency<T> createDependency)
			: base(typeof(T))
		{
			if (createDependency == null)
			{
				throw new ArgumentNullException(nameof(createDependency));
			}

			this.CreateDependency = createDependency;
		}

		internal CreateDependency<T> CreateDependency { get; }
	}
}
