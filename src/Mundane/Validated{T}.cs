using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Mundane
{
	/// <summary>The predicate to evaluate.</summary>
	/// <param name="value">The value to evaluate.</param>
	/// <typeparam name="T">The type of the value to evaluate.</typeparam>
	public delegate bool ValidationPredicate<in T>([DisallowNull] T value)
		where T : notnull;

	/// <summary>Async version of the predicate to evaluate.</summary>
	/// <param name="value">The value to evaluate.</param>
	/// <typeparam name="T">The type of the value to evaluate.</typeparam>
	public delegate ValueTask<bool> ValidationPredicateAsync<in T>([DisallowNull] T value)
		where T : notnull;

	/// <summary>Represents a validated value.</summary>
	/// <typeparam name="T">The type of the validated value.</typeparam>
	public sealed class Validated<T> : Validated, IEquatable<Validated<T>>
		where T : notnull
	{
		private readonly T value;

		internal Validated(T value)
		{
			this.value = value;
		}

		/// <summary>Implicitly converts the validated value to its underlying type.</summary>
		/// <param name="value">The validated value.</param>
		/// <returns>The value converted to its underlying type.</returns>
		[return: NotNullIfNotNull("value")]
		public static implicit operator T([DisallowNull] Validated<T>? value)
		{
			return value != null ? value.value : default!;
		}

		/// <summary>Implicitly converts the underlying type to its validated value equivalent.</summary>
		/// <param name="value">The underlying type value.</param>
		/// <returns>The underlying type value converted to its validated value equivalent.</returns>
		[return: NotNullIfNotNull("value")]
		public static implicit operator Validated<T>([DisallowNull] T value)
		{
			return value != null ? new Validated<T>(value) : null!;
		}

		/// <summary>Equality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if equal, otherwise false.</returns>
		public static bool operator ==([AllowNull] Validated<T>? left, [AllowNull] Validated<T>? right)
		{
			if (left is null)
			{
				return right is null;
			}

			return left.Equals(right);
		}

		/// <summary>Inequality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if not equal, otherwise false.</returns>
		public static bool operator !=([AllowNull] Validated<T>? left, [AllowNull] Validated<T>? right)
		{
			return !(left == right);
		}

		/// <inheritdoc/>
		public override bool Equals([AllowNull] object? obj)
		{
			if (obj is Validated<T> other)
			{
				return this.value.Equals(other.value);
			}

			return false;
		}

		/// <inheritdoc/>
		public bool Equals([AllowNull] Validated<T>? other)
		{
			return other is not null && this.value.Equals(other.value);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return HashCode.Combine(this.value);
		}

		/// <inheritdoc/>
		public override string? ToString()
		{
			return this.value.ToString();
		}

		/// <summary>Validates a value. If the predicate returns false, the error message is added to this value's collection of error messages.</summary>
		/// <param name="predicate">The predicate to evaluate.</param>
		/// <param name="errorMessage">The error message to add when the predicate returns false.</param>
		/// <returns>The validated value.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="predicate"/> or <paramref name="errorMessage"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public Validated<T> Validate(
			[DisallowNull] ValidationPredicate<T> predicate,
			[DisallowNull] string errorMessage)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException(nameof(predicate));
			}

			if (errorMessage == null)
			{
				throw new ArgumentNullException(nameof(errorMessage));
			}

			if (!predicate.Invoke(this.value))
			{
				this.AddErrorMessage(errorMessage);
			}

			return this;
		}

		/// <summary>Validates a value. If the predicate returns false, the error message is added to this value's collection of error messages.</summary>
		/// <param name="predicate">The predicate to evaluate.</param>
		/// <param name="errorMessage">The error message to add when the predicate returns false.</param>
		/// <returns>The validated value.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="predicate"/> or <paramref name="errorMessage"/> is <see langword="null"/>.</exception>
		public async ValueTask<Validated<T>> Validate(
			[DisallowNull] ValidationPredicateAsync<T> predicate,
			[DisallowNull] string errorMessage)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException(nameof(predicate));
			}

			if (errorMessage == null)
			{
				throw new ArgumentNullException(nameof(errorMessage));
			}

			if (!await predicate.Invoke(this.value))
			{
				this.AddErrorMessage(errorMessage);
			}

			return this;
		}
	}
}
