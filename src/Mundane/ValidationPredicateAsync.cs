using System.Threading.Tasks;

namespace Mundane;

/// <summary>Async version of the predicate to evaluate.</summary>
/// <param name="value">The value to evaluate.</param>
/// <typeparam name="T">The type of the value to evaluate.</typeparam>
public delegate ValueTask<bool> ValidationPredicateAsync<in T>(T value)
	where T : notnull;
