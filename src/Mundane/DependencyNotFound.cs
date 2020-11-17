using System;

namespace Mundane
{
	/// <summary>The exception thrown when requesting a dependency type which can not be resolved.</summary>
	public sealed class DependencyNotFound : Exception
	{
		internal DependencyNotFound(Type dependencyType)
			: base(DependencyNotFound.CreateMessage(dependencyType))
		{
		}

		private static string CreateMessage(Type dependencyType)
		{
			return $"No concrete type has been registered for \"{dependencyType.Name}\".";
		}
	}
}
