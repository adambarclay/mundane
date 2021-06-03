using System;
using System.Threading.Tasks;

namespace Mundane
{
	/// <summary>Additional methods to allow the chaining of calls to the asynchronous version of Validate().</summary>
	public static class ValidatedAsyncExtensions
	{
		/// <summary>Validates a value. If the predicate returns false, the error message is added to the value's collection of error messages.</summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="task">The task representing the previous validation operation on the value.</param>
		/// <param name="predicate">The predicate to evaluate.</param>
		/// <param name="errorMessage">The error message to add when the predicate returns false.</param>
		/// <returns>The validated value.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="predicate"/> or <paramref name="errorMessage"/> is <see langword="null"/>.</exception>
		/// <exception cref="ValidationReturnedNull">The validation returns a <see langword="null"/> <typeparamref name="T"/>.</exception>
		public static async ValueTask<Validated<T>> Validate<T>(
			this ValueTask<Validated<T>> task,
			ValidationPredicateAsync<T> predicate,
			string errorMessage)
			where T : notnull
		{
			if (predicate is null)
			{
				throw new ArgumentNullException(nameof(predicate));
			}

			if (errorMessage is null)
			{
				throw new ArgumentNullException(nameof(errorMessage));
			}

			var model = await task;

			if (model is null)
			{
				throw new ValidationReturnedNull("The validation returned null.");
			}

			return await model.Validate(predicate, errorMessage);
		}

		/// <summary>Validates a value. If the predicate returns false, the error message is added to the value's collection of error messages.</summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="task">The task representing the previous validation operation on the value.</param>
		/// <param name="predicate">The predicate to evaluate.</param>
		/// <param name="errorMessage">The error message to add when the predicate returns false.</param>
		/// <returns>The validated value.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="predicate"/> or <paramref name="errorMessage"/> is <see langword="null"/>.</exception>
		/// <exception cref="ValidationReturnedNull">The validation returns a <see langword="null"/> <typeparamref name="T"/>.</exception>
		public static async ValueTask<Validated<T>> Validate<T>(
			this ValueTask<Validated<T>> task,
			ValidationPredicate<T> predicate,
			string errorMessage)
			where T : notnull
		{
			if (predicate is null)
			{
				throw new ArgumentNullException(nameof(predicate));
			}

			if (errorMessage is null)
			{
				throw new ArgumentNullException(nameof(errorMessage));
			}

			var model = await task;

			if (model is null)
			{
				throw new ValidationReturnedNull("The validation returned null.");
			}

			return model.Validate(predicate, errorMessage);
		}
	}
}
