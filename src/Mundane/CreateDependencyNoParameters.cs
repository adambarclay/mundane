namespace Mundane
{
	/// <summary>The method for creating an object which fulfils the dependency receiving no parameters.</summary>
	/// <typeparam name="T">The type of the dependency.</typeparam>
	/// <returns>An instance of the dependency registered for the requested type <typeparamref name="T"/>.</returns>
	public delegate T CreateDependencyNoParameters<out T>();
}
