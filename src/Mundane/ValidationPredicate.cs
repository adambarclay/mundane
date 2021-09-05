namespace Mundane
{
	/// <summary>The predicate to evaluate.</summary>
	/// <param name="value">The value to evaluate.</param>
	/// <typeparam name="T">The type of the value to evaluate.</typeparam>
	public delegate bool ValidationPredicate<in T>(T value)
		where T : notnull;
}
