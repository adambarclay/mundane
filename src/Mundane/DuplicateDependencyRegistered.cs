using System;

namespace Mundane;

/// <summary>The exception thrown when attempting to register a dependency type more than once.</summary>
public sealed class DuplicateDependencyRegistered : Exception
{
	/// <summary>Initializes a new instance of the <see cref="DuplicateDependencyRegistered"/> class.</summary>
	/// <param name="dependencyType">The type of the dependency.</param>
	public DuplicateDependencyRegistered(Type dependencyType)
		: base(DuplicateDependencyRegistered.CreateMessage(dependencyType))
	{
	}

	private static string CreateMessage(Type dependencyType)
	{
		ArgumentNullException.ThrowIfNull(dependencyType);

		return $"The type \"{dependencyType.Name}\" has been registered more than once.";
	}
}
