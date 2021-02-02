using System;
using System.Collections;
using System.Collections.Generic;

namespace Mundane
{
	/// <summary>Wraps a dictionary to allow enumeration without modification.</summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	public readonly struct EnumerableDictionary<TKey, TValue>
		: IEnumerable<KeyValuePair<TKey, TValue>>, IEquatable<EnumerableDictionary<TKey, TValue>>
		where TKey : notnull
		where TValue : notnull
	{
		/// <summary>Initializes a new instance of the <see cref="EnumerableDictionary{TKey, TValue}"/> struct.</summary>
		/// <param name="dictionary">The underlying dictionary.</param>
		/// <exception cref="ArgumentNullException"><paramref name="dictionary"/> is <see langword="null"/>.</exception>
		public EnumerableDictionary(Dictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException(nameof(dictionary));
			}

			this.Dictionary = dictionary;
		}

		internal Dictionary<TKey, TValue> Dictionary { get; }

		/// <summary>Implicitly converts a <see cref="Dictionary{TKey, TValue}"/> to an <see cref="EnumerableDictionary{TKey,TValue}"/>.</summary>
		/// <param name="dictionary">The <see cref="Dictionary{TKey, TValue}"/>.</param>
		/// <returns>The <see cref="EnumerableDictionary{TKey,TValue}"/>.</returns>
		public static implicit operator EnumerableDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
		{
			return new EnumerableDictionary<TKey, TValue>(dictionary);
		}

		/// <summary>Equality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if equal, otherwise false.</returns>
		public static bool operator ==(
			EnumerableDictionary<TKey, TValue> left,
			EnumerableDictionary<TKey, TValue> right)
		{
			return left.Equals(right);
		}

		/// <summary>Inequality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if not equal, otherwise false.</returns>
		public static bool operator !=(
			EnumerableDictionary<TKey, TValue> left,
			EnumerableDictionary<TKey, TValue> right)
		{
			return !left.Equals(right);
		}

		/// <inheritdoc/>
		public override bool Equals(object? obj)
		{
			return obj is EnumerableDictionary<TKey, TValue> other && this.Equals(other);
		}

		/// <inheritdoc/>
		public bool Equals(EnumerableDictionary<TKey, TValue> other)
		{
			return this.Dictionary == other.Dictionary;
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this.Dictionary.GetEnumerator();
		}

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <inheritdoc/>
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return this.Dictionary.GetHashCode();
		}
	}
}
