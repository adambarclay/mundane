using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mundane
{
	/// <summary>Manages the validation of input values.</summary>
	public sealed class Validator
	{
		private readonly List<Validated> values;

		private Validator()
		{
			this.values = new List<Validated>();
		}

		/// <summary>Validates an object.</summary>
		/// <param name="validationOperation">The validation to perform, typically constructing a validated object.</param>
		/// <typeparam name="T">The type of object being validated.</typeparam>
		/// <returns>The result of the validation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="validationOperation"/> is <see langword="null"/>.</exception>
		/// <exception cref="ValidationReturnedNull">The validation returns a <see langword="null"/> <typeparamref name="T"/>.</exception>
		public static ValidationResult<T> Validate<T>(ValidationOperation<T> validationOperation)
			where T : notnull
		{
			if (validationOperation is null)
			{
				throw new ArgumentNullException(nameof(validationOperation));
			}

			var validator = new Validator();

			var model = validationOperation(validator);

			if (model is null)
			{
				throw new ValidationReturnedNull("The validation returned null.");
			}

			return new ValidationResult<T>(validator.values.Any(validatedValue => validatedValue.Failed), model);
		}

		/// <summary>Validates an object.</summary>
		/// <param name="validationOperation">The validation to perform, typically constructing a validated object.</param>
		/// <typeparam name="T">The type of object being validated.</typeparam>
		/// <returns>The result of the validation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="validationOperation"/> is <see langword="null"/>.</exception>
		/// <exception cref="ValidationReturnedNull">The validation result is a <see langword="null"/> <typeparamref name="T"/>.</exception>
		public static async ValueTask<ValidationResult<T>> Validate<T>(
			ValidationOperation<ValueTask<T>> validationOperation)
			where T : notnull
		{
			if (validationOperation is null)
			{
				throw new ArgumentNullException(nameof(validationOperation));
			}

			var validation = new Validator();

			var model = await validationOperation.Invoke(validation);

			if (model is null)
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
		public Validated<T> Value<T>(T value)
			where T : notnull
		{
			if (value is null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			var validatedValue = new Validated<T>(value, value.ToString() ?? string.Empty);

			this.values.Add(validatedValue);

			return validatedValue;
		}

		/// <summary>Begins the validation of a value by converting from a string to another type. Chain call Validate methods to check additional contraints.</summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value to validate.</param>
		/// <param name="convert">Converts the value from string to another type.</param>
		/// <param name="fallbackValue">The value to use when the conversion fails.</param>
		/// <param name="errorMessage">The error message to add when the conversion fails.</param>
		/// <returns>The validated value.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="value"/> or <paramref name="convert"/> is <see langword="null"/>.</exception>
		public Validated<T> Value<T>(
			string value,
			ValidateConvertReturn<T> convert,
			T fallbackValue,
			string errorMessage)
			where T : notnull
		{
			if (value is null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (convert is null)
			{
				throw new ArgumentNullException(nameof(convert));
			}

			Validated<T> validatedValue;

			try
			{
				validatedValue = new Validated<T>(convert(value), value);
			}
			catch
			{
				validatedValue = new Validated<T>(fallbackValue, value);
				validatedValue.AddErrorMessage(errorMessage);
			}

			this.values.Add(validatedValue);

			return validatedValue;
		}

		/// <summary>Begins the validation of a value by converting from a string to another type. Chain call Validate methods to check additional contraints.</summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="value">The value to validate.</param>
		/// <param name="convert">Converts the value from string to another type.</param>
		/// <param name="fallbackValue">The value to use when the conversion fails.</param>
		/// <param name="errorMessage">The error message to add when the conversion fails.</param>
		/// <returns>The validated value.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="value"/> or <paramref name="convert"/> is <see langword="null"/>.</exception>
		public Validated<T> Value<T>(string value, ValidateConvertOut<T> convert, T fallbackValue, string errorMessage)
			where T : notnull
		{
			if (value is null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (convert is null)
			{
				throw new ArgumentNullException(nameof(convert));
			}

			Validated<T> validatedValue;

			if (convert(value, out var convertedValue))
			{
				validatedValue = new Validated<T>(convertedValue, value);
			}
			else
			{
				validatedValue = new Validated<T>(fallbackValue, value);
				validatedValue.AddErrorMessage(errorMessage);
			}

			this.values.Add(validatedValue);

			return validatedValue;
		}
	}
}
