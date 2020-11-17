using System;

namespace Mundane
{
	/// <summary>The exception thrown when attempting to register a dependency type more than once.</summary>
	public sealed class DuplicateDependencyRegistered : Exception
	{
		internal DuplicateDependencyRegistered(Type dependencyType)
			: base(DuplicateDependencyRegistered.CreateMessage(dependencyType))
		{
		}

		private static string CreateMessage(Type dependencyType)
		{
			return $"The type \"{dependencyType.Name}\" has been registered more than once.";
		}
	}
}
