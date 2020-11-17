using System;
using System.Diagnostics;

namespace Mundane
{
	/// <summary>Contains the base type of the dependency for which a concrete type will be created when requested.</summary>
	public abstract class Dependency
	{
		internal Dependency(Type dependencyType)
		{
			Debug.Assert(dependencyType != null);

			this.DependencyType = dependencyType;
		}

		internal Type DependencyType { get; }
	}
}
