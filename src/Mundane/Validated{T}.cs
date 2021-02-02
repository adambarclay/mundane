using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Mundane
{
	/// <summary>The predicate to evaluate.</summary>
	/// <param name="value">The value to evaluate.</param>
	/// <typeparam name="T">The type of the value to evaluate.</typeparam>
	public delegate bool ValidationPredicate<in T>(T value)
		where T : notnull;

	/// <summary>Async version of the predicate to evaluate.</summary>
	/// <param name="value">The value to evaluate.</param>
	/// <typeparam name="T">The type of the value to evaluate.</typeparam>
	public delegate ValueTask<bool> ValidationPredicateAsync<in T>(T value)
		where T : notnull;

	/// <summary>Represents a validated value.</summary>
	/// <typeparam name="T">The type of the validated value.</typeparam>
	public class Validated<T> : Validated, IEquatable<Validated<T>>
		where T : notnull
	{
		private readonly string stringValue;

		internal Validated(string stringValue)
		{
			this.stringValue = stringValue;
		}

		/// <summary>Gets the validated value.</summary>
		/// <exception cref="InvalidOperationException">The validated value has not been set.</exception>
		protected virtual T Value
		{
			get
			{
				throw new InvalidOperationException("A validated value has not been set.");
			}
		}

		/// <summary>Implicitly converts the validated value to its underlying type.</summary>
		/// <param name="value">The validated value.</param>
		/// <returns>The value converted to its underlying type.</returns>
		[return: NotNullIfNotNull("value")]
		public static implicit operator T?(Validated<T> value)
		{
			return value is not null ? value.Value : default;
		}

		/// <summary>Implicitly converts the underlying type to its validated value equivalent.</summary>
		/// <param name="value">The underlying type value.</param>
		/// <returns>The underlying type value converted to its validated value equivalent.</returns>
		public static implicit operator Validated<T>(string value)
		{
			return new Validated<T>(value ?? string.Empty);
		}

		/// <summary>Equality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if equal, otherwise false.</returns>
		public static bool operator ==(Validated<T>? left, Validated<T>? right)
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
		public static bool operator !=(Validated<T>? left, Validated<T>? right)
		{
			return !(left == right);
		}

		/// <inheritdoc/>
		public override bool Equals(object? obj)
		{
			if (obj is Validated<T> other)
			{
				return this.Value.Equals(other.Value);
			}

			return false;
		}

		/// <inheritdoc/>
		public bool Equals(Validated<T>? other)
		{
			return other is not null && this.Value.Equals(other.Value);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Value);
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			return this.stringValue;
		}

		/// <summary>Validates a value. If the predicate returns false, the error message is added to this value's collection of error messages.</summary>
		/// <param name="predicate">The predicate to evaluate.</param>
		/// <param name="errorMessage">The error message to add when the predicate returns false.</param>
		/// <returns>The validated value.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="predicate"/> or <paramref name="errorMessage"/> is <see langword="null"/>.</exception>
		public Validated<T> Validate(ValidationPredicate<T> predicate, string errorMessage)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException(nameof(predicate));
			}

			if (errorMessage == null)
			{
				throw new ArgumentNullException(nameof(errorMessage));
			}

			if (!predicate.Invoke(this.Value))
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
		public async ValueTask<Validated<T>> Validate(ValidationPredicateAsync<T> predicate, string errorMessage)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException(nameof(predicate));
			}

			if (errorMessage == null)
			{
				throw new ArgumentNullException(nameof(errorMessage));
			}

			if (!await predicate.Invoke(this.Value))
			{
				this.AddErrorMessage(errorMessage);
			}

			return this;
		}
	}
}
