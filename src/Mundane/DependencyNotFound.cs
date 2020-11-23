using System;
using System.Diagnostics.CodeAnalysis;

namespace Mundane
{
	/// <summary>The exception thrown when requesting a dependency type which can not be resolved.</summary>
	public sealed class DependencyNotFound : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="DependencyNotFound"/> class.</summary>
		/// <param name="dependencyType">The type of the dependency.</param>
		public DependencyNotFound([DisallowNull] Type dependencyType)
			: base(DependencyNotFound.CreateMessage(dependencyType))
		{
		}

		private static string CreateMessage(Type dependencyType)
		{
			if (dependencyType == null)
			{
				throw new ArgumentNullException(nameof(dependencyType));
			}

			return $"No concrete type has been registered for \"{dependencyType.Name}\".";
		}
	}
}
