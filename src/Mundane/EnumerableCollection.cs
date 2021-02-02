using System;
using System.Collections;
using System.Collections.Generic;

namespace Mundane
{
	/// <summary>Wraps a collection to allow enumeration without modification.</summary>
	/// <typeparam name="T">The type of the elements in the collection.</typeparam>
	public readonly struct EnumerableCollection<T> : IEnumerable<T>, IEquatable<EnumerableCollection<T>>
		where T : notnull
	{
		/// <summary>Initializes a new instance of the <see cref="EnumerableCollection{T}"/> struct.</summary>
		/// <param name="collection">The underlying collection.</param>
		/// <exception cref="ArgumentNullException"><paramref name="collection"/> is <see langword="null"/>.</exception>
		public EnumerableCollection(List<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}

			this.Collection = collection;
		}

		internal List<T> Collection { get; }

		/// <summary>Equality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if equal, otherwise false.</returns>
		public static bool operator ==(EnumerableCollection<T> left, EnumerableCollection<T> right)
		{
			return left.Equals(right);
		}

		/// <summary>Inequality operator.</summary>
		/// <param name="left">The left side of the operation.</param>
		/// <param name="right">The right side of the operation.</param>
		/// <returns>true if not equal, otherwise false.</returns>
		public static bool operator !=(EnumerableCollection<T> left, EnumerableCollection<T> right)
		{
			return !left.Equals(right);
		}

		/// <inheritdoc/>
		public bool Equals(EnumerableCollection<T> other)
		{
			return this.Collection == other.Collection;
		}

		/// <inheritdoc/>
		public override bool Equals(object? obj)
		{
			return obj is EnumerableCollection<T> other && this.Equals(other);
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public List<T>.Enumerator GetEnumerator()
		{
			return this.Collection.GetEnumerator();
		}

		/// <inheritdoc/>
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return this.Collection.GetHashCode();
		}
	}
}
