using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Mundane
{
	/// <summary>The validation to perform, typically constructing a validated object.</summary>
	/// <param name="validator">Manages the validation of input values.</param>
	/// <typeparam name="T">The type of object being validated.</typeparam>
	[return: NotNull]
	public delegate T ValidatorDelegate<out T>([DisallowNull] Validator validator);

	/// <summary>Manages the validation of input values.</summary>
	public sealed class Validator
	{
		private readonly List<Validated> values;

		private Validator()
		{
			this.values = new List<Validated>();
		}

		/// <summary>Validates an object.</summary>
		/// <param name="validate">The validation to perform, typically constructing a validated object.</param>
		/// <typeparam name="T">The type of object being validated.</typeparam>
		/// <returns>The result of the validation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="validate"/> is <see langword="null"/>.</exception>
		/// <exception cref="ValidationReturnedNull">The validation returns a <see langword="null"/> <typeparamref name="T"/>.</exception>
		[return: NotNull]
		public static ValidationResult<T> Validate<T>([DisallowNull] ValidatorDelegate<T> validate)
			where T : notnull
		{
			if (validate == null)
			{
				throw new ArgumentNullException(nameof(validate));
			}

			var validator = new Validator();

			var model = validate(validator);

			if (model == null)
			{
				throw new ValidationReturnedNull("The validation returned null.");
			}

			return new ValidationResult<T>(validator.values.Any(validatedValue => validatedValue.Failed), model);
		}

		/// <summary>Validates an object.</summary>
		/// <param name="validate">The validation to perform, typically constructing a validated object.</param>
		/// <typeparam name="T">The type of object being validated.</typeparam>
		/// <returns>The result of the validation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="validate"/> is <see langword="null"/>.</exception>
		/// <exception cref="ValidationReturnedNull">The validation result is a <see langword="null"/> <typeparamref name="T"/>.</exception>
		public static async ValueTask<ValidationResult<T>> Validate<T>(
			[DisallowNull] ValidatorDelegate<ValueTask<T>> validate)
			where T : notnull
		{
			if (validate == null)
			{
				throw new ArgumentNullException(nameof(validate));
			}

			var validation = new Validator();

			var model = await validate.Invoke(validation);

			if (model == null)
			{
				throw new ValidationReturnedNull("The validation returned null.");
			}

			return new ValidationResult<T>(validation.values.Any(validatedValue => validatedValue.Failed), model);
		}

		/// <summary>Begins the validation of a value. Chain call Validate methods to check additional contraints.</summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value to validate.</param>
		/// <returns>The validated value.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public Validated<T> Value<T>([DisallowNull] T value)
			where T : notnull
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			var validatedValue = new Validated<T>(value);

			this.values.Add(validatedValue);

			return validatedValue;
		}
	}
}
