using System;
using System.ComponentModel;

namespace Mundane
{
	/// <summary>The result of a validation.</summary>
	/// <typeparam name="TModel">The type of object being validated.</typeparam>
	public readonly struct ValidationResult<TModel> : IEquatable<ValidationResult<TModel>>
		where TModel : notnull
	{
		internal ValidationResult(bool invalid, TModel model)
		{
			this.Invalid = invalid;
			this.Model = model;
		}

		/// <summary>Gets a value indicating whether the model is invalid.</summary>
		public bool Invalid { get; }

		/// <summary>Gets the model object created during validation.</summary>
		public TModel Model { get; }

		/// <summary>Equality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if equal, otherwise false.</returns>
		public static bool operator ==(ValidationResult<TModel> left, ValidationResult<TModel> right)
		{
			return left.Equals(right);
		}

		/// <summary>Inequality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if not equal, otherwise false.</returns>
		public static bool operator !=(ValidationResult<TModel> left, ValidationResult<TModel> right)
		{
			return !(left == right);
		}

		/// <summary>Deconstructs a <see cref="ValidationResult{TModel}"/>.</summary>
		/// <param name="invalid">A value indicating whether the model is invalid.</param>
		/// <param name="model">The model object created during validation.</param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Deconstruct(out bool invalid, out TModel model)
		{
			invalid = this.Invalid;
			model = this.Model;
		}

		/// <inheritdoc/>
		public override bool Equals(object? obj)
		{
			if (obj is ValidationResult<TModel> other)
			{
				return this.Equals(other);
			}

			return false;
		}

		/// <inheritdoc/>
		public bool Equals(ValidationResult<TModel> other)
		{
			return this.Invalid.Equals(other.Invalid) && this.Model.Equals(other.Model);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Invalid, this.Model);
		}
	}
}
