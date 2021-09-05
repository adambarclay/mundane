namespace Mundane
{
	/// <summary>The method for creating an object which fulfils the dependency using the current request.</summary>
	/// <typeparam name="T">The type of the dependency.</typeparam>
	/// <param name="request">The current request.</param>
	/// <returns>An instance of the dependency registered for the requested type <typeparamref name="T"/>.</returns>
	public delegate T CreateDependency<out T>(Request request);
}
