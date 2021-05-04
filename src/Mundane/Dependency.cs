using System;

namespace Mundane
{
	/// <summary>Contains the base type of the dependency for which a concrete type will be created when requested.</summary>
	public abstract class Dependency
	{
		internal Dependency(Type dependencyType)
		{
			this.DependencyType = dependencyType;
		}

		internal Type DependencyType { get; }
	}
}
